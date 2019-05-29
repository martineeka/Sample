using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.OrgUnit;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]

    public class OrganizationUnitController : ControllerBase
    {
        private readonly IOrganizationUnitService _organizationUnitProvider;
        private readonly IBrowseSuperiorProviderService _browseSuperiorProvider;
        private readonly IJobTitleService _jobTitleProvider;
        //private readonly ILocationService _locationProvider;
        private IMapper _mapper;

        public OrganizationUnitController(IOrganizationUnitService organizationUnitProvider, IBrowseSuperiorProviderService browseSuperiorProvider,
        IJobTitleService jobTitleProvider, IMapper mapper)
        {
            _organizationUnitProvider = organizationUnitProvider;
            _browseSuperiorProvider = browseSuperiorProvider;
            _jobTitleProvider = jobTitleProvider;


            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var orgUnits = _organizationUnitProvider.GetListOrganizationUnit();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = orgUnits.ToDataSourceResult(req);

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
            var orgUnit = _organizationUnitProvider.Get(Id);
            if (orgUnit == null)
            {
                return NotFound("Organization unit not found.");
            }
           
            return Ok(orgUnit);
        }

        [HttpGet]
        [Route("GetOrgUnit/{Id}")]
        public IActionResult GetOrgUnit(int Id)
        {
            var orgUnit = _organizationUnitProvider.Get(Id);
            if (orgUnit == null)
            {
                return NotFound("Organization unit not found.");
            }

            return Ok(orgUnit);
        }

        [HttpGet]
        [Route("GetSelectedEmployee/{Id}")]
        public IActionResult GetSelectedEmployee(int Id)
        {
            var selectedEmployee = _browseSuperiorProvider.GetSelectedEmployee((int)Id);
           

            return Ok(selectedEmployee);
        }

        [HttpGet]
        [Route("GetCurrentEmployeeFullName/{Id}")]
        public IActionResult GetCurrentEmployeeFullName(int Id)
        {
            var selectedEmployee = _browseSuperiorProvider.GetCurrentEmployeeFullName((int)Id);


            return Ok(selectedEmployee);
        }

        [HttpGet]
        [Route("GetLegalEntityCreateEditViewModel/{model}")]
        public CreateEditViewModel GetLegalEntityCreateEditViewModel(CreateEditViewModel model)
        {
            var legalEntity = _organizationUnitProvider.GetLegalEntityInformation(model.LegalEntityInformationID.GetValueOrDefault());
            _mapper.Map(legalEntity, model);

            return model;
        }
        //public LegalEntityViewModel GetLegalEntityViewModel(LegalEntityViewModel model)
        //{
        //    var legalEntity = GetLegalEntityInformation(model.LegalEntityInformationID.GetValueOrDefault());
        //    _mapper.Map(legalEntity, model);

        //    return model;
        //}

        [HttpGet]
        [Route("GetLegalEntity/{Id}")]
        public IActionResult GetLegalEntity(string orgUnitID)
        {
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(orgUnitID));

            var viewModel = new LegalEntityViewModel();
            var orgUnit = _organizationUnitProvider.Get(decryptedID);
            var legalEntity = _organizationUnitProvider.GetLegalEntityInformation(orgUnit.LegalEntityInformationId.Value);

            _mapper.Map(orgUnit, viewModel);
            viewModel.ID = orgUnitID;
            viewModel.LegalEntityInformationID = legalEntity.Id;

            //viewModel = GetLegalEntityViewModel(viewModel);

            viewModel.StructuralSuperiorName = orgUnit.SuperiorId != null
                ? _browseSuperiorProvider.GetCurrentEmployeeFullName((int)orgUnit.SuperiorId)
                : "";
            viewModel.ManagingDirectorName =
                _browseSuperiorProvider.GetCurrentEmployeeFullName(orgUnit.LegalEntityInformation.ManagingDirectorId);


            return Ok(viewModel);
        }

        [HttpGet]
        [Route("GetLegalEntityInformation/{id}")]
        public LegalEntityInformation GetLegalEntityInformation(int id)
        {
            var legalEntity = _organizationUnitProvider.GetLegalEntityInformation(id);
            

            return legalEntity;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            OrgUnit orgUnit = new OrgUnit();

            if (model.SuperiorID != null)
            {
                model.SuperiorID = EncryptionHelper.DecryptUrlParam(model.SuperiorID);

                int headerID = Int32.Parse(model.SuperiorID);
            }
            if (model.ParentID != null)
            {
                model.ParentID = EncryptionHelper.DecryptUrlParam(model.ParentID);
                int parent = Int32.Parse(model.ParentID);

            }

            var orgUnitMapper = _mapper.Map(model, orgUnit);
            _organizationUnitProvider.Add(orgUnitMapper);
            return Ok(model);
        }

        [HttpPost]
        [Route("AddWithLegalEntity")]
        public IActionResult AddWithLegalEntity([FromBody] CreateEditViewModel model)
        {
            var orgUnit = new OrgUnit();
            _mapper.Map(model, orgUnit);
            var legalEntity = new LegalEntityInformation();
            model.SuperiorID = EncryptionHelper.DecryptUrlParam(model.SuperiorID);
            var legalEntityMapper = _mapper.Map(model, legalEntity);
            legalEntity.ManagingDirectorId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ManagingDirectorID));
            _organizationUnitProvider.AddLegalEntity(legalEntityMapper);

            orgUnit.LegalEntityInformation = legalEntity;
            //if (model.AttachmentFile != null)
            //{
            //    var theObject = new OrgUnit();
            //    var tableName = GetTableName(theObject);

            //    var attachment = new Attachment
            //    {
            //        TableOriginID = orgUnit.ID,
            //        TableOriginName = tableName,
            //        AttachmentFile = model.AttachmentFile
            //    };

            //model.SuperiorID = !string.IsNullOrEmpty(model.SuperiorID) ? EncryptionHelper.DecryptUrlParam(model.SuperiorID) : null;

            var orgUnitMapper = _mapper.Map(model, orgUnit);
            _organizationUnitProvider.Add(orgUnitMapper);

            return Ok(orgUnitMapper);
            }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            model.ID = EncryptionHelper.DecryptUrlParam(model.ID);
            var orgUnit = _organizationUnitProvider.Get(Convert.ToInt32(model.ID));

            model.ParentID = string.IsNullOrEmpty(model.ParentID) ?
                  (string)null : EncryptionHelper.DecryptUrlParam(model.ParentID);

            model.SuperiorID = string.IsNullOrEmpty(model.SuperiorID) ?
                 (string)null : EncryptionHelper.DecryptUrlParam(model.SuperiorID);

            var orgUnitMApper = _mapper.Map(model, orgUnit);
            _organizationUnitProvider.Edit(orgUnitMApper);
            return Ok();
        }

        [HttpPut]
        [Route("EditWithLegalEntity")]
        public IActionResult EditWithLegalEntity([FromBody] CreateEditViewModel model)
        {
            var orgUnit = _organizationUnitProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, orgUnit);
            var legalEntity = new LegalEntityInformation();
            if (model.LegalEntityInformationID.HasValue)
            {
                legalEntity = _organizationUnitProvider.GetLegalEntityInformation(model.LegalEntityInformationID.GetValueOrDefault());
                var legalEntityMapper = _mapper.Map(model, legalEntity);

               _organizationUnitProvider.EditLegalEntity(legalEntityMapper);
            }
            else
            {
                var legalEntityMapper = _mapper.Map(model, legalEntity);

               _organizationUnitProvider.AddLegalEntity(legalEntityMapper);
              
            }
            #region Attachment
            //if (model.AttachmentFile != null)
            //{
            //    var theObject = new OrgUnit();
            //    var tableName = GetTableName(theObject);

            //    if (legalEntity.AttachmentID.HasValue)
            //    {
            //        var attachment = GetAttachment(legalEntity.AttachmentID.GetValueOrDefault());
            //        attachment.TableOriginID = orgUnit.ID;
            //        attachment.TableOriginName = tableName;
            //        attachment.AttachmentFile = model.AttachmentFile;

            //        SetAuditFields(attachment);

            //        legalEntity.Attachment = attachment;
            //    }
            //    else
            //    {
            //        var attachment = new Attachment();
            //        attachment.TableOriginID = orgUnit.ID;
            //        attachment.TableOriginName = tableName;
            //        attachment.AttachmentFile = model.AttachmentFile;

            //        context.Attachment.Add(attachment);
            //        SetAuditFields(attachment);

            //        legalEntity.Attachment = attachment;
            //    }

            //}
            #endregion
            orgUnit.LegalEntityInformation = legalEntity;
            var orgUnitMapper = _mapper.Map(model, orgUnit);
            _organizationUnitProvider.Edit(orgUnitMapper);

            return Ok(orgUnitMapper);
        }

        [HttpPut]
        [Route("EditWithAddLegalEntity")]
        public IActionResult EditWithAddLegalEntity(OrgUnit orgUnit, LegalEntityInformation legalEntity)
        {
            _organizationUnitProvider.AddLegalEntity(legalEntity);
            orgUnit.LegalEntityInformation = legalEntity;
            _organizationUnitProvider.Edit(orgUnit);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                foreach (var ID in arrayOfID)
                {
                    if (_organizationUnitProvider.ValidateDelete(ID))
                    {
                        return BadRequest("Unable to delete! Selected data has been used on another menu");
                    }
                }
                Array.ForEach(listID, _organizationUnitProvider.Delete);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete]
        [Route("DeleteLegalEntity/ID")]
        public IActionResult DeleteLegalEntity(string ID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(ID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
               
                Array.ForEach(listID, _organizationUnitProvider.DeleteLegalEntity);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("ValidateCombination")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _organizationUnitProvider.validateCombination(model);
            return Ok(isValid);
        }

        [HttpGet]
        [Route("CostCenter")]
        public IActionResult GetCostCenter()
        {
            var data = _organizationUnitProvider.GetCostCenters();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("OrganizationUnitLevel")]
        public IActionResult GetOrganizationUnitLevel()
        {
            var data = _organizationUnitProvider.GetOrganizationLevel();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("OrganizationUnitCategory")]
        public IActionResult GetOrganizationUnitCategory()
        {
            var data = _organizationUnitProvider.GetOrganizationUnitCategory();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetCurrency")]
        public IActionResult GetCurrency()
        {
            var data = _organizationUnitProvider.GetCurrency();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetKPPBranch")]
        public IActionResult GetKPPBranch()
        {
            var data = _organizationUnitProvider.GetKppBranches();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetBusinessFieldRegulation")]
        public IActionResult GetBusinessFieldRegulation()
        {
            var data = _organizationUnitProvider.GetBusinessFieldRegulations();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetBusinessFieldCategories/{businessFieldRegulationID}/{text}")]
        public IActionResult GetBusinessFieldCategories(int businessFieldRegulationID , string text)
        {
            var getBusinessCategory = _organizationUnitProvider.GetBusinessFieldCategories(businessFieldRegulationID);

            if (!string.IsNullOrEmpty(text))
            {
                getBusinessCategory = getBusinessCategory.Where(s => s.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return Ok(getBusinessCategory.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetBusinessFieldClassification/{businessFieldCategoryID}/{text}")]
        public IActionResult GetBusinessFieldClassification(int businessFieldCategoryID, string text)
        {
            var getBusinessClassification = _organizationUnitProvider.GetBusinessFieldClassifications(businessFieldCategoryID);

            var lookUp = getBusinessClassification.Select(
               classification =>
                   new
                   {
                       Value = classification.Id,
                       Text =
                           string.Format("{0}{1}{2}{3} - {4}", classification.MainSectionCode,
                               classification.SectionCode, classification.SubSectionCode,
                               classification.GroupCode, classification.GroupName)
                   });
            if (!string.IsNullOrEmpty(text))
            {
                lookUp = lookUp.Where(s => s.Text.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
            return Ok(lookUp);
        }

        [HttpGet]
        [Route("GetSIUPClasses")]
        public IActionResult GetSIUPClasses()
        {
            var data = _organizationUnitProvider.GetSIUPCLasses();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("ParentalOrgUnit/{id}/{categoryTypeID}/{text}")]
        public IActionResult GetParentalOrgUnit(string id, int categoryTypeID,string text)
        {
            var decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var data = _organizationUnitProvider.GetParentalOrgUnits(decryptedID, categoryTypeID);

            if (!string.IsNullOrEmpty(text))
            {
                data = data.Where(s => s.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        #region ListJobTitle
        [HttpGet]
        [Route("GetIndexJobTitle/{organizationID}")]
        public IActionResult GetIndexJobTitle(int organizationID)
        {
            var viewModel = new IndexJobTitleViewModel();
            var grades = _organizationUnitProvider.Get(organizationID);


            return Ok(viewModel);
        }

        [HttpPost]
        [Route("GetListJobTitle/{organizationID}")]
        public IActionResult GetListJobTitle([FromBody]CustomDataSourceRequest request, int organizationID)
        {
            try
            {
                var jobTitle = _organizationUnitProvider.ListJobTitle(organizationID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = jobTitle.ToDataSourceResult(req);

                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("GetJobTitle/{Id}")]
        public IActionResult GetJobTitle(int Id)
        {
            var jobTitle = _organizationUnitProvider.GetJobTitleDetail(Id);
            if (jobTitle == null)
            {
                return NotFound("Job Title not found.");
            }

            return Ok(jobTitle);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddJobTitle/{model}")]
        public IActionResult AddJobTitle([FromBody] CreateEditJobTitleViewModel model)
        {
            var orgListJobTitle = new OrganizationListJobtitle();
            if (model is null)
            {
                return BadRequest("Job Title is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            //jobTitle.OrganizationID = EncryptionHelper.DecryptUrlParam(jobTitle.OrganizationID);
            var jobTitleMapper = _mapper.Map(model, orgListJobTitle);
            orgListJobTitle.Id = 0;
            orgListJobTitle.OrganizationId = Convert.ToInt32(model.OrganizationID);
            orgListJobTitle.BeginEff = model.ValidFrom;
            orgListJobTitle.LastEff = model.ValidTo;
            _organizationUnitProvider.AddJobTitle(jobTitleMapper);
            var orgID = orgListJobTitle.Id.ToString();
            return Ok(orgID);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("EditJobTitle")]
        public IActionResult EditJobTitle([FromBody] CreateEditJobTitleViewModel model)
        {
            if (model == null)
            {
                return BadRequest(" Job Title is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var jobTitle = _organizationUnitProvider.GetJobTitleDetail(Convert.ToInt32(model.ID));
            var gradeMapper = _mapper.Map(model, jobTitle);
            _organizationUnitProvider.EditJobTitle(gradeMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("DeleteJobTitle/{arrayOfID}")]
        public IActionResult DeleteJobTitle(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _organizationUnitProvider.DeleteJobTitle);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("DropDownJobTitle")]
        public IActionResult GetDropDownJobTitle()
        {
            var data = _jobTitleProvider.Get();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }
        #endregion

        #region ListLocation
        [HttpGet]
        [Route("GetIndexLocation/{groupGradeID}")]
        public IActionResult GetIndexLocation(int organizationID)
        {
            //int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(groupGradeID));
            var viewModel = new IndexLocationViewModel();
            //var grades = _groupGradeProvider.Get(decryptID);
            var grades = _organizationUnitProvider.Get(organizationID);


            return Ok(viewModel);
        }

        [HttpPost]
        [Route("GetListLocation/{organizationID}")]
        public IActionResult GetListLocation([FromBody]CustomDataSourceRequest request, int organizationID)
        {
            try
            {
                var location = _organizationUnitProvider.ListLocation(organizationID);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = location.ToDataSourceResult(req);

                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpGet]
        [Route("GetLocation/{Id}")]
        public IActionResult GetLocation(int Id)
        {
            var location = _organizationUnitProvider.GetLocationDetail(Id);
            if (location == null)
            {
                return NotFound("Location not found.");
            }

            return Ok(location);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("AddLocation")]
        public IActionResult AddLocation([FromBody] CreateEditJobTitleViewModel location)
        {
            var model = new OrganizationListLocation();
            if (location is null)
            {
                return BadRequest("Location is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            location.OrganizationID = EncryptionHelper.DecryptUrlParam(location.OrganizationID);
            int headerID = Int32.Parse(location.OrganizationID);

            var locationMapper = _mapper.Map(location, model);
            _organizationUnitProvider.AddLocation(locationMapper);
            return Ok(location);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("EditLocation")]
        public IActionResult EditLocation([FromBody] CreateEditLocationViewModel model)
        {
            if (model == null)
            {
                return BadRequest(" Location is null.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var location = _organizationUnitProvider.GetLocationDetail(Convert.ToInt32(model.ID));
            var locationMapper = _mapper.Map(model, location);
            _organizationUnitProvider.EditLocation(locationMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("DeleteLocation/{arrayOfID}")]
        public IActionResult DeleteLocation(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                Array.ForEach(listID, _organizationUnitProvider.DeleteLocation);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("DropDownLocation")]
        public IActionResult GetDropDownLocation()
        {
            var data = _organizationUnitProvider.GetLocationMaster();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }
        #endregion


    }
}