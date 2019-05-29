using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.OrgUnitChange;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class OrganizationUnitChangeController : ControllerBase
    {
        private readonly IOrganizationUnitChangeService _orgUnitChangeProvider;
        private IMapper _mapper;

        public OrganizationUnitChangeController(IOrganizationUnitChangeService jobTitleProvider, IMapper mapper)
        {
            _orgUnitChangeProvider = jobTitleProvider;
            _mapper = mapper;
        }

        #region OrganizationUnitChange
        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                //DataClaim.GetClaim(Request);
                var orgUnitChange = _orgUnitChangeProvider.List();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = orgUnitChange.ToDataSourceResult(req);

                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("Get/{Id}")]
        public IActionResult Get(int Id)
        {
            var jobTitle = _orgUnitChangeProvider.GetOrgUnitTrans(Id);
            if (jobTitle == null)
            {
                return NotFound("Organization Unit Change not found.");
            }

            return Ok(jobTitle);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            if (model is null)
            {
                return BadRequest("Organization Unit Change is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            DataClaim.GetClaim(Request);

            OrgUnitTransaction orgUnitChange = new OrgUnitTransaction();
            var menuMapper = _mapper.Map(model, orgUnitChange);
            menuMapper.OrgUnitId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID));
            menuMapper.RequestDate = DateTime.Today;
            menuMapper.TransactionStatusId = 10;
            _orgUnitChangeProvider.Add(menuMapper);

            //get OrgUnitChild dari OrgUnit tsb, dan save ke detail
            var child = _orgUnitChangeProvider.GetOrgUnitChild(menuMapper.OrgUnitId);
            _orgUnitChangeProvider.AddChildOrgUnit(menuMapper, child);

            return Ok(model);
        }

        // PUT api/<controller>
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Organization Unit Change is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();

            var title = _orgUnitChangeProvider.Get(decryptedID);
            var menuMapper = _mapper.Map(model, title);
            menuMapper.OrgUnitId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID));
            _orgUnitChangeProvider.Edit(menuMapper);
            return Ok();
        }

        // DELETE api/<controller>/json
        [HttpDelete]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            DataClaim.GetClaim(Request);
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            AjaxViewModel model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    if (_orgUnitChangeProvider.ValidateDelete(ID))
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on another menu";
                        return BadRequest(model);

                        //return BadRequest("Unable to delete! Selected data has been used on another menu");
                    }
                }
                Array.ForEach(listID, _orgUnitChangeProvider.Delete);
                model.IsSuccess = true;
                model.Message = "Delete Seuccessfully";
            }
            catch (Exception)
            {
                throw;
            }
            return Ok(model);
        }


        [HttpPost]
        [Route("ValidateCombination")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _orgUnitChangeProvider.ValidateCombination(model);
            return Ok(isValid);
        }
        #endregion OrganizationUnitChange

        #region OrganizationUnitChangeDetail
        [HttpGet]
        [Route("CostCenter")]
        public IActionResult GetCostCenter()
        {
            var data = _orgUnitChangeProvider.GetCostCenter();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("OrgLevel")]
        public IActionResult GetOrgLevel()
        {
            var data = _orgUnitChangeProvider.GetOrgLevel();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("LegalEntityInformation")]
        public IActionResult GetLegalEntityInformation()
        {
            var data = _orgUnitChangeProvider.GetLegalEntityInformation();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Sppkpnumber }));
        }

        [HttpGet]
        [Route("Category")]
        public IActionResult GetCategory()
        {
            var data = _orgUnitChangeProvider.GetCategory();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetDetailIndex/{Id}")]
        public IActionResult GetDetailIndex(int Id)
        {
            var jobTitle = _orgUnitChangeProvider.GetDetailIndex(Id);
            if (jobTitle == null)
            {
                return NotFound("Job Title not found.");
            }

            return Ok(jobTitle);
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("GetDetailList/{id}")]
        public IActionResult GetDetailList([FromBody]CustomDataSourceRequest request, int id)
        {
            try
            {
                //DataClaim.GetClaim(Request);

                var orgUnitChange = _orgUnitChangeProvider.GetDetailList(id);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = orgUnitChange.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetDetail/{Id}")]
        public IActionResult GetDetail(int id)
        {
            var jobTitle = _orgUnitChangeProvider.GetOrgChangeDetail(id);
            if (jobTitle == null)
            {
                return NotFound("Organization Unit Change Detail not found.");
            }

            CreateEditDetailViewModel detail = new CreateEditDetailViewModel();
            var menuMapper = _mapper.Map(jobTitle, detail);
            menuMapper.EmployeeID = EncryptionHelper.EncryptUrlParam(Convert.ToString(jobTitle.SuperiorId));
            menuMapper.EmployeeName = _orgUnitChangeProvider.GetActiveEmployeeBiodata(Convert.ToInt32(jobTitle.SuperiorId)).NickName;
            menuMapper.OrganizationUnitID = EncryptionHelper.EncryptUrlParam(Convert.ToString(jobTitle.ParentId));
            menuMapper.OrganizationUnit = jobTitle.ParentId == 0 ? "" : _orgUnitChangeProvider.GetOrgUnit(Convert.ToInt32(jobTitle.ParentId)).Name;
            return Ok(menuMapper);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddDetail")]
        public IActionResult PostDetail([FromBody] CreateEditDetailViewModel model)
        {
            //DataClaim.GetClaim(Request);
            if (model is null)
            {
                return BadRequest("Organization Unit Detail Change is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            OrgUnitTransactionDetail detail = new OrgUnitTransactionDetail();
            var menuMapper = _mapper.Map(model, detail);
            menuMapper.SuperiorId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.EmployeeID));
            menuMapper.ParentId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID));
            menuMapper.StatusId = OrgChangeStatus.Add;
            menuMapper.IsCurrentUsed = true;
            //menuMapper.IsActive = true;

            _orgUnitChangeProvider.AddOrgUnitChangeDetail(menuMapper);
            return Ok(model);
        }

        // PUT api/<controller>
        [HttpPut]
        [Route("EditDetail")]
        public IActionResult PutDetail([FromBody] CreateEditDetailViewModel model)
        {
            //DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Organization Unit Change Detail is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();

            var title = _orgUnitChangeProvider.GetOrgChangeDetail(decryptedID);
            var menuMapper = _mapper.Map(model, title);
            menuMapper.SuperiorId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.EmployeeID));
            menuMapper.ParentId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID));
            menuMapper.StatusId = OrgChangeStatus.Change;
            menuMapper.Id = 0;
            menuMapper.IsCurrentUsed = true;
            //menuMapper.IsActive = true;

            //_orgUnitChangeProvider.ChangeIsActive(model.OrgUnitID);
            _orgUnitChangeProvider.EditOrgUnitChangeDetail(menuMapper);
            return Ok();
        }

        // PUT api/<controller>
        [HttpPut]
        [Route("DeadActive")]
        public IActionResult DeadActiveDetail([FromBody] CreateEditDetailViewModel model)
        {
            //DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Organization Unit Change Detail is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();

            var title = _orgUnitChangeProvider.GetOrgChangeDetail(decryptedID);
            var menuMapper = _mapper.Map(model, title);
            menuMapper.SuperiorId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.EmployeeID));
            menuMapper.ParentId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID));
            menuMapper.StatusId = OrgChangeStatus.NotActive;
            menuMapper.Id = 0;
            menuMapper.IsCurrentUsed = true;
            //menuMapper.IsActive = false;

            _orgUnitChangeProvider.ChangeIsCurrentUsed(model.OrgUnitID);
            _orgUnitChangeProvider.DeadActiveDetail(menuMapper);
            return Ok();
        }

        // DELETE api/<controller>/json
        [HttpDelete]
        [Route("DeleteDetail/{arrayOfID}")]
        public IActionResult DeleteDetail(string arrayOfID)
        {
            DataClaim.GetClaim(Request);
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                foreach (var ID in listID)
                {
                    if (_orgUnitChangeProvider.ValidateDeleteOrgUnitChangeDetail(ID))
                    {
                        return BadRequest("Unable to delete! Selected data has been used on another menu");
                    }
                }
                Array.ForEach(listID, _orgUnitChangeProvider.Delete);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }
        #endregion OrganizationUnitChangeDetail
    }
}