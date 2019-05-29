using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.ResponsibilityGroup;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    //[Authorize]
    [Route("api/Security/[controller]")]
    [ApiController]
    public class ResponsibilityGroupController : ControllerBase
    {
        private readonly IResponsibilityGroupService _responibilityGroupProvider;
        private readonly IResponsibilityService _responibilityProvider;
        private readonly IRespGroupDetailService _resGroupDetailProvider;
        private IMapper _mapper;

        public ResponsibilityGroupController(IResponsibilityGroupService responibilityGroupProvider,
                                                IMapper mapper,
                                                IResponsibilityService responibilityProvider,
                                                IRespGroupDetailService resGroupDetailProvider)
        {
            _responibilityGroupProvider = responibilityGroupProvider;
            _mapper = mapper;
            _responibilityProvider = responibilityProvider;
            _resGroupDetailProvider = resGroupDetailProvider;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var jobMasters = _responibilityGroupProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = jobMasters.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {

                throw;
            }

        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            var responsibilitygroup = _responibilityGroupProvider.Get();
            return Ok(responsibilitygroup);
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var responsibilitygroup = _responibilityGroupProvider.Get(Id);
            if (responsibilitygroup == null)
            {
                return NotFound("Responsibility Group not found.");
            }

            return Ok(responsibilitygroup);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel resGroup)
        {
            if (resGroup is null)
            {
                return BadRequest("Responsibility Group is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var resGroupMapper = _mapper.Map<ResponsibilityGroup>(resGroup);
            _responibilityGroupProvider.Add(resGroupMapper);
            return Ok(resGroupMapper);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel resGroup)
        {
            if (resGroup == null)
            {
                return BadRequest("ResponsibilityGroup is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int ID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(resGroup.ID));
            resGroup.ID = ID.ToString();
            var resGroups = _responibilityGroupProvider.Get(Convert.ToInt32(ID));
            var resGroupMapper = _mapper.Map(resGroup, resGroups);
            _responibilityGroupProvider.Edit(resGroupMapper);
            return Ok();
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
                foreach (var ID in listID)
                {
                    var IsUsed = _responibilityGroupProvider.IsUsedByUser(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _responibilityGroupProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _responibilityGroupProvider.validateCombination(model);
            return Ok(isValid);
        }

        [HttpGet]
        [Route("GetResponsibilityGroup/{resGroupID}")]
        public IActionResult GetResponsibilityGroup(int resGroupID)
        {
            var viewModel = new IndexRespGroupDetailViewModel();
            var resGroup = _responibilityGroupProvider.Get(resGroupID);

            if (resGroup != null)
            {
                viewModel.ID = resGroup.Id.ToString();
                viewModel.ResGroupCode = resGroup.Code;
                viewModel.ResGroupName = resGroup.Name;
            }
            return Ok(viewModel);
        }

        [HttpPost]
        [Route("GetListRespGroupDetail/{resGroupID}")]
        public IActionResult GetListRespGroupDetail([FromBody]CustomDataSourceRequest request, int resGroupID)
        {
            try
            {
                var grades = _responibilityGroupProvider.ListRespGroupDetail(resGroupID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = grades.ToDataSourceResult(req);

                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetResponsibility")]
        public IActionResult GetResponsibility()
        {
            var data = _responibilityProvider.Get();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpPost]
        [Route("AddRespGroupDetail")]
        public IActionResult AddGradeDetail([FromBody] CreateEditRespGroupDetailViewModel RespGroupDetail)
        {
            var model = new RespGroupDetail();
            if (RespGroupDetail is null)
            {
                return BadRequest("Responsibility Group Detail is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //grade.GroupGradeID = EncryptionHelper.DecryptUrlParam(grade.GroupGradeID);
            //int headerID = Int32.Parse(grade.GroupGradeID);
            var gradeMapper = _mapper.Map(RespGroupDetail, model);
            _resGroupDetailProvider.Add(gradeMapper);
            return Ok();
        }

        [HttpGet]
        [Route("GetRespGroupDetail/{Id}")]
        public IActionResult GetRespGroupDetail(int Id)
        {
            var resGroupDetail = _resGroupDetailProvider.Get(Id);
            if (resGroupDetail == null)
            {
                return NotFound("Responsibility Group Detail not found.");
            }

            return Ok(resGroupDetail);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("EditRespGroupDetail")]
        public IActionResult EditRespGroupDetail([FromBody] CreateEditRespGroupDetailViewModel model)
        {
            if (model == null)
            {
                return BadRequest(" Responsibility Group Detail is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int DecryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            var resGroupDetail = _resGroupDetailProvider.Get(DecryptID);
            model.ID = DecryptID.ToString();
            var gradeMapper = _mapper.Map(model, resGroupDetail);
            _resGroupDetailProvider.Edit(gradeMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("DeleteRespGroupDetail/{arrayOfID}")]
        public IActionResult DeleteRespGroupDetail(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _resGroupDetailProvider.Delete);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ValidateIsAxists")]
        public IActionResult ValidateIsAxists([FromBody]CreateEditRespGroupDetailViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _resGroupDetailProvider.validateIsAxist(model);
            return Ok(isValid);
        }
    }
}