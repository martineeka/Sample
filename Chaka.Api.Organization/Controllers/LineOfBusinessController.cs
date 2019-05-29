using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.ViewModels.Organization.LineOfBusiness;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chaka.Database.Context.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class LineOfBusinessController : ControllerBase
    {
        private ILineOfBusinessServices _LineOfBusinessProvider;
        private IMapper _mapper;

        public LineOfBusinessController(ILineOfBusinessServices LineOfBusinessProvider, IMapper mapper)
        {
            _LineOfBusinessProvider = LineOfBusinessProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _LineOfBusinessProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = levels.ToDataSourceResult(req);

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
            var data = _LineOfBusinessProvider.Get(Id);
            if (data == null)
            {
                return NotFound("Data not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(data, model);
            return Ok(data);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            var data = new LineOfBusiness();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, data);
            _LineOfBusinessProvider.Add(data);
            return Ok(data);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var data = _LineOfBusinessProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, data);
            _LineOfBusinessProvider.Edit(data);
            return Ok(data);
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _LineOfBusinessProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }


        [HttpGet]
        [Route("IsCodeValid/{code}/{id}")]
        public IActionResult IsCodeValid(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _LineOfBusinessProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpGet]
        [Route("IsCodeExist/{code}")]
        public IActionResult IsCodeExist(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _LineOfBusinessProvider.IsCodeExist(code);
            return Ok(isLevelCodeValid);
        }
    }
}