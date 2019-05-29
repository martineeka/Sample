using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.Location;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationProvider;
        private IMapper _mapper;

        public LocationController(ILocationService locationProvider, IMapper imapper)
        {
            _locationProvider = locationProvider;
            _mapper = imapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var locations = _locationProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = locations.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var model = new CreateEditViewModel();
            var location = _locationProvider.Get(Id);
            if (location == null)
            {
                return NotFound("Location not found.");
            }

            model.ID = Id.ToString();
            var locationMapper = _mapper.Map(location, model);
            return Ok(locationMapper);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            var location = new Location();
            if (model is null)
            {
                return BadRequest("Location is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var locationMapper = _mapper.Map(model, location);
            _locationProvider.Add(locationMapper);
            return Ok(locationMapper);
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Location is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var location = _locationProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, location);
            _locationProvider.Edit(location);
            return Ok();
        }

        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _locationProvider.Delete);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("ValidateLocationCode/{code}/{id}")]
        public IActionResult ValidateLocationCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var IsLocationCodeValid = _locationProvider.IsLocationCodeValid(code, decryptID);
            return Ok(IsLocationCodeValid);
        }


        [HttpGet]
        [Route("GetListCountry")]
        public IActionResult GetListCountry()
        {
            var data = _locationProvider.GetListCountry();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));

        }

        [HttpGet]
        [Route("GetListState/{CountryID}")]
        public IActionResult GetListState(string CountryID)
        {
            var data = _locationProvider.GetListState(Convert.ToInt32(CountryID));
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));

        }

        [HttpGet]
        [Route("GetListCity/{ProvinceId}")]
        public IActionResult GetListCity(string ProvinceId)
        {
            var data = _locationProvider.GetListCity(Convert.ToInt32(ProvinceId));
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));

        }

        //GetListClassification
        [HttpGet]
        [Route("GetListClassification")]
        public IActionResult GetListClassification()
        {
            var data = _locationProvider.GetListClassification();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));

        }

    }
}