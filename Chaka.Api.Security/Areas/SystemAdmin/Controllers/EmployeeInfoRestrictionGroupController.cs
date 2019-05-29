using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.EmployeeInfoRestrictionGroup;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    [Route("api/Security/[controller]")]
    public class EmployeeInfoRestrictionGroupController : ControllerBase
    {
        IEmployeeInfoRestrictionGroupService _employeeRestrictionProvider;
        IMapper _mapper;
        public EmployeeInfoRestrictionGroupController(IEmployeeInfoRestrictionGroupService service, IMapper mapper)
        {
            this._employeeRestrictionProvider = service;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var menus = _employeeRestrictionProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = menus.ToDataSourceResult(req);

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
            var restrict = _employeeRestrictionProvider.Get(Id);
            if (restrict == null)
            {
                return NotFound("Info Restriction not found.");
            }

            return Ok(restrict);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel restrict)
        {
            DataClaim.GetClaim(Request);
            if (restrict is null)
            {
                return BadRequest("Info Restriction is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newRestrict = new EmployeeInfoRestrictionGroup();
            _mapper.Map(restrict, newRestrict);
            _employeeRestrictionProvider.Add(newRestrict);
            return Ok(newRestrict);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel restrict)
        {
            DataClaim.GetClaim(Request);
            if (restrict == null)
            {
                return BadRequest("Menu is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var restrictGet = _employeeRestrictionProvider.Get(Convert.ToInt32(restrict.ID));
            _mapper.Map(restrict, restrictGet);
            _employeeRestrictionProvider.Edit(restrictGet);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            var model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    var ValidateDelete = _employeeRestrictionProvider.ValidateDelete(ID);
                    if (ValidateDelete.IsSuccess)
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on " + ValidateDelete.Message + " menu";

                        return BadRequest(model);
                    }
                }
                foreach (int id in listID)
                {
                    _employeeRestrictionProvider.Delete(id);
                }
                model.IsSuccess = true;
                model.Message = "Delete Successfully";
                //Array.ForEach(listID, _employeeRestrictionProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(model);
        }

        [HttpGet]
        [Route("ValidateCode/{code}/{id}")]
        public IActionResult ValidateCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _employeeRestrictionProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _employeeRestrictionProvider.ValidateCombination(model);
            return Ok(isValid);
        }
    }
}