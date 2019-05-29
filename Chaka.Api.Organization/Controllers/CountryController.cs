using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.Country;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        public ICountryService _countryProvider;
        public IProvinceService _provinceProvider;
        public ICityService _cityProvider;
        public IMapper _mapper;

        public CountryController(ICountryService service, 
            IProvinceService provinceProvider, 
            IMapper mapper,
            ICityService cityProvider)
        {
            this._countryProvider = service;
            this._mapper = mapper;
            this._provinceProvider = provinceProvider;
            this._cityProvider = cityProvider;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var country = _countryProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = country.ToDataSourceResult(req);
                return Ok(filter);
            }

            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("GetListProvince/{countryID}")]
        public IActionResult GetListProvince([FromBody]CustomDataSourceRequest request,int countryID)
        {
            try
            {
                var province = _provinceProvider.GetListByCountryID(countryID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = province.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("GetListCity/{provinceID}")]
        public IActionResult GetListCity([FromBody]CustomDataSourceRequest request, int provinceID)
        {
            try
            {
                var city = _cityProvider.GetList(provinceID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = city.ToDataSourceResult(req);
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
            var country = _countryProvider.Get(Id);
            if (country == null)
            {
                return NotFound("Country not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(country, model);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetProvince/{Id}")]
        public IActionResult GetProvince(int Id)
        {
            var model = new CreateEditProvinceViewModel();
            var province = _provinceProvider.Get(Id);
            if (province == null)
            {
                return NotFound("Province not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(province, model);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetCity/{Id}")]
        public IActionResult GetCity(int Id)
        {
            var model = new CreateEditCityViewModel();
            var city = _cityProvider.Get(Id);
            if (city == null)
            {
                return NotFound("City not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(city, model);
            return Ok(model);
        }

        [HttpGet]
        [Route("GetIndexCity/{provinceID}")]
        public IActionResult GetIndexCity(int provinceID)
        {
            var model = new IndexCityViewModel();
            var province = _provinceProvider.Get(provinceID);
            var country = _countryProvider.Get(province.CountryId);
            if(province == null)
            {
                return NotFound("Province not found");
            }

            model.CountryName = country.Name;
            model.CountryCode = country.Code;
            model.ProvinceName = province.Name;
            return Ok(model);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            var country = new Country();
            if (model is null)
            {
                return BadRequest("Country is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, country);
            _countryProvider.Add(country);
            return Ok(country);
        }

        [HttpPost]
        [Route("AddProvince/{countryID}")]
        public IActionResult PostProvince([FromBody] CreateEditProvinceViewModel model,int countryID)
        {
            DataClaim.GetClaim(Request);
            var province = new Province();
            model.CountryID = countryID.ToString();
            if (model is null)
            {
                return BadRequest("Province is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, province);
            _provinceProvider.Add(province);
            return Ok(province);
        }

        [HttpPost]
        [Route("AddCity/{provinceID}")]
        public IActionResult PostCity([FromBody] CreateEditCityViewModel model,int provinceID)
        {
            DataClaim.GetClaim(Request);
            var city = new City();
            model.ProvinceID = provinceID.ToString();
            if (model is null)
            {
                return BadRequest("City is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, city);
            _cityProvider.Add(city);
            return Ok(city);
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Country is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var country = _countryProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, country);
            _countryProvider.Edit(country);
            return Ok();
        }

        [HttpPut]
        [Route("EditProvince")]
        public IActionResult PutProvince([FromBody] CreateEditProvinceViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Province is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var province = _provinceProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, province);
            _provinceProvider.Edit(province);
            return Ok();
        }

        [HttpPut]
        [Route("EditCity")]
        public IActionResult PutCity([FromBody] CreateEditCityViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("City is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var city = _cityProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, city);
            _cityProvider.Edit(city);
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
                Array.ForEach(listID, _countryProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("DeleteProvince/{arrayOfID}")]
        public IActionResult DeleteProvince(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _provinceProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("DeleteCity/{arrayOfID}")]
        public IActionResult DeleteCity(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _cityProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("ValidateCode/{code}/{id}")]
        public IActionResult ValidateCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _countryProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _countryProvider.ValidateCombination(model);
            return Ok(isValid);
        }

        [HttpGet]
        [Route("ValidateProvinceCode/{code}/{id}/{countryID}")]
        public IActionResult ValidateProvinceCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _provinceProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("ValidateProvince")]
        public IActionResult ValidateProvinceCombination([FromBody]CreateEditProvinceViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _provinceProvider.ValidateCombination(model);
            return Ok(isValid);
        }

        [HttpGet]
        [Route("ValidateCityCode/{code}/{id}/{provinceID}")]
        public IActionResult ValidateCityCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _cityProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("ValidateCity")]
        public IActionResult ValidateCityCombination([FromBody]CreateEditCityViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _cityProvider.ValidateCombination(model);
            return Ok(isValid);
        }
    }
}