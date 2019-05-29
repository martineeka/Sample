using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.User;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    [Route("api/Security/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _UserProvider;
        private IMapper _mapper;

        public UserController(IUserService UserProvider, IMapper mapper)
        {
            _UserProvider = UserProvider;
            _mapper = mapper;
        }
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _UserProvider.GetList();

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
            var data = _UserProvider.GetDetail(Id);
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
            var data = new User();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, data);
            _UserProvider.Add(data);
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
            var data = _UserProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, data);
            _UserProvider.Edit(data);
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
                Array.ForEach(listID, _UserProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("UserValidateCombination")]
        public IActionResult UserValidateCombination([FromBody] CreateEditViewModel model)
        {
            var entity = new User();
            _mapper.Map(model, entity);
            var isValid = _UserProvider.UserValidateCombination(entity);
            return Ok(isValid);
        }

        [HttpPost]
        [Route("GetListEmployee")]
        public IActionResult GetListEmployee([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var Employee = _UserProvider.GetListEmployee();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = Employee.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetEmployee")]
        public IActionResult GetEmployee()
        {
            var data = _UserProvider.GetEmployee();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetEmployeeListFilter")]
        public IActionResult GetEmployeeListFilter()
        {
            var data = _UserProvider.GetEmployeeListFilter();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetResponsibilityGroup")]
        public IActionResult GetResponsibilityGroup()
        {
            var data = _UserProvider.GetResponsibilityGroup();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetPreferenceGroup")]
        public IActionResult GetPreferenceGroup()
        {
            var data = _UserProvider.GetPreferenceGroup();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetRestrictionGroup")]
        public IActionResult GetRestrictionGroup()
        {
            var data = _UserProvider.GetRestrictionGroup();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetUserStatus")]
        public IActionResult GetUserStatus()
        {
            var data = _UserProvider.GetUserStatus();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

    }
}