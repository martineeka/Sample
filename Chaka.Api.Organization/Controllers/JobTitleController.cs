using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobTitle;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Newtonsoft.Json;
using Chaka.Database.Context.Models;
using System.Collections.Generic;
using Chaka.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _jobTitleProvider;
        private IMapper _mapper;


        public JobTitleController(IJobTitleService jobTitleProvider, IMapper mapper)
        {
            _jobTitleProvider = jobTitleProvider;
            _mapper = mapper;
        }

        #region JobTitle
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var jobTitle = _jobTitleProvider.List();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = jobTitle.ToDataSourceResult(req);
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
            var jobTitle = _jobTitleProvider.Get(Id);
            if (jobTitle == null)
            {
                return NotFound("Job Title not found.");
            }

            return Ok(jobTitle);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel jobTitle)
        {
            if (jobTitle is null)
            {
                return BadRequest("Job Title is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            JobTitle jt = new JobTitle();
            var menuMapper = _mapper.Map(jobTitle, jt);
            _jobTitleProvider.Add(menuMapper);
            return Ok(jobTitle);
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel jobTitle)
        {
            if (jobTitle == null)
            {
                return BadRequest("Job Title is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(jobTitle.ID));
            jobTitle.ID = decryptedID.ToString();

            var title = _jobTitleProvider.Get(decryptedID);
            var menuMapper = _mapper.Map(jobTitle, title);
            _jobTitleProvider.Edit(menuMapper);
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            AjaxViewModel model = new AjaxViewModel();
            try
            {
                foreach (var ID in listID)
                {
                    if (_jobTitleProvider.ValidateDelete(ID))
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on another menu";

                        return BadRequest(model);
                        ////return BadRequest("Unable to delete! Selected data has been used on another menu");
                    }
                }
                Array.ForEach(listID, _jobTitleProvider.Delete);
                model.IsSuccess = true;
                model.Message = "Delete Successfully";
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(model);
        }

        [HttpGet]
        [Route("JobStatus")]
        public IActionResult GetJobStatus()
        {
            var data = _jobTitleProvider.GetJobStatus();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("Level")]
        public IActionResult GetLevel()
        {
            var data = _jobTitleProvider.GetLevel();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("LevelCategory/{levelID}")]
        public IActionResult GetLevelCategory(int levelID)
        {
            var data = _jobTitleProvider.GetLevelCategory(levelID);
            if (data == null)
            {
                data.Value = "0";
                data.Text = "";
            }
            return Ok(data);
        }

        [HttpGet]
        [Route("GetJobFamily")]
        public IActionResult GetJobFamily()
        {
            var data = _jobTitleProvider.GetJobFamily();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("ValidateJobTitleCode/{code}/{id}")]
        public IActionResult ValidateJobTitleCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isJobTitleCodeValid = _jobTitleProvider.IsJobTitleCodeValid(code, decryptID);
            return Ok(isJobTitleCodeValid);
        }

        [HttpPost]
        [Route("ValidateCombination")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _jobTitleProvider.ValidateCombination(model);
            return Ok(isValid);
        }

        [HttpGet]
        [Route("GetDataDescription/{JobTitleID}")]
        public IActionResult GetDataDescription(string JobTitleID)
        {
            var decryptID = Convert.ToInt32(JobTitleID);
            var jobTitle = _jobTitleProvider.GetDetailIndex(decryptID);

            var viewModel = new IndexNewPageViewModel();

            int[] IDs = null;
            IDs = _jobTitleProvider.GetJobTitleID(decryptID);

            viewModel.ListUnSelectedDescription = _jobTitleProvider.ListUnselected(decryptID, IDs);
            if (viewModel.ListUnSelectedDescription == null)
            {
                viewModel.ListUnSelectedDescription = new List<ListDescriptionViewModel>();
            }

            IDs = viewModel.ListUnSelectedDescription.Select(s => s.ID).ToArray();
            viewModel.ListSelectedDescription = _jobTitleProvider.ListSelected(decryptID, IDs);
            if (viewModel.ListSelectedDescription == null)
            {
                viewModel.ListSelectedDescription = new List<ListDescriptionViewModel>();
            }

            return Ok(viewModel);
        }

        #endregion JobTitle

        [HttpGet]
        [Route("GetDetail/{Id}")]
        public IActionResult GetDetail(int Id)
        {
            var jobTitle = _jobTitleProvider.GetDetailIndex(Id);
            if (jobTitle == null)
            {
                return NotFound("Job Title not found.");
            }

            return Ok(jobTitle);
        }

        #region JobDescription

        [HttpGet]
        [Route("GetDetailDesc/{JobTitleID}")]
        public IActionResult Index(string JobTitleID)
        {
            var decryptID = Convert.ToInt32(JobTitleID);
            var jobTitle = _jobTitleProvider.GetDetailIndex(decryptID);

            var viewModel = new IndexDetailViewModel();
            viewModel.JobTitleID = decryptID;
            viewModel.JobTitleCode = jobTitle.JobTitleCode;
            viewModel.JobTitleName = jobTitle.JobTitleName;
            viewModel.Description = jobTitle.Description;
            viewModel.JobStatus = jobTitle.JobStatus;
            viewModel.JobLevel = jobTitle.JobLevel;
            viewModel.JobLevelCategory = jobTitle.JobLevelCategory;
            viewModel.BeginEff = jobTitle.BeginEff;
            viewModel.LastEff = jobTitle.LastEff;

            int[] IDs = null;
            IDs = _jobTitleProvider.GetJobTitleID(decryptID);

            viewModel.ListUnSelected = _jobTitleProvider.ListUnselected(decryptID, IDs);
            if (viewModel.ListUnSelected == null)
            {
                viewModel.ListUnSelected = new List<ListDescriptionViewModel>();
            }

            IDs = viewModel.ListUnSelected.Select(s => s.ID).ToArray();
            viewModel.ListSelected = _jobTitleProvider.ListSelected(decryptID, IDs);
            if (viewModel.ListSelected == null)
            {
                viewModel.ListSelected = new List<ListDescriptionViewModel>();
            }

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("ListUnselected/{JobTitleID}/{selectedID}")]
        public ActionResult ListUnselected([FromBody] CustomDataSourceRequest request, int JobTitleID, string selectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _jobTitleProvider.ListUnselected(JobTitleID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpPost]
        [Route("ListSelected/{JobTitleID}/{unselectedID}")]
        public ActionResult ListSelected([FromBody] CustomDataSourceRequest request, int JobTitleID, string unselectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(unselectedID);
                var result = _jobTitleProvider.ListSelected(JobTitleID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        [Route("UpdateDescription")]
        public ActionResult UpdateDescription([FromBody]IndexDetailViewModel model)
        {
            try
            {
                _jobTitleProvider.UpdateListJobTitle(model.JobTitleID, model.Selected);
                return Ok();
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
                return BadRequest(ajaxViewModel);
            }
        }

        [HttpPost]
        [Route("GetDescriptionList/{id}")]
        public IActionResult GetDescriptionList([FromBody]CustomDataSourceRequest request, int id)
        {
            try
            {
                var jobTitle = _jobTitleProvider.GetDescriptionList(id);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = jobTitle.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("GetDescription/{Id}")]
        public IActionResult GetDescription(int Id)
        {
            var jobTitle = _jobTitleProvider.GetDescription(Id);
            if (jobTitle == null)
            {
                return NotFound("Job Title not found.");
            }

            return Ok(jobTitle);
        }
        #endregion JobDescription
        #region JobSpesification

        [HttpPost]
        [Route("GetSpesificationList/{id}")]
        public IActionResult GetSpesificationList([FromBody]CustomDataSourceRequest request, int id)
        {
            try
            {
                var jobTitle = _jobTitleProvider.GetSpesificationList(id);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = jobTitle.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetDetailSpecification/{encryptedTitleID}/{encryptedTitleSpecificationID}")]
        public IActionResult GetDetailSpecification(string encryptedTitleID, string encryptedTitleSpecificationID)
        {
            var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(encryptedTitleID));
            var decryptSpecificationID = 0;
            if (encryptedTitleSpecificationID != null && encryptedTitleSpecificationID != "0")
            {
                decryptSpecificationID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(encryptedTitleSpecificationID));
            }

            var JobSpecification = _jobTitleProvider.GetSpesification(decryptSpecificationID);

            var viewModel = new CreateEditSpecificationViewModel();
            _mapper.Map(JobSpecification, viewModel);
            viewModel.JobTitleID = decryptID.ToString();
            viewModel.EncryptedJobTitleID = encryptedTitleID;

            int[] IDs = null;
            IDs = _jobTitleProvider.GetJobTitleSpecificationID(decryptSpecificationID);

            viewModel.ListUnSelected = _jobTitleProvider.ListMajorUnselected(decryptSpecificationID, IDs);
            if (viewModel.ListUnSelected == null)
            {
                viewModel.ListUnSelected = new List<ListDescriptionViewModel>();
            }

            IDs = viewModel.ListUnSelected.Select(s => s.ID).ToArray();
            viewModel.ListSelected = _jobTitleProvider.ListMajorSelected(decryptSpecificationID, IDs);
            if (viewModel.ListSelected == null)
            {
                viewModel.ListSelected = new List<ListDescriptionViewModel>();
            }

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("ListMajorUnselected/{JobTitleID}/{selectedID}")]
        public ActionResult ListMajorUnselected([FromBody] CustomDataSourceRequest request, int JobTitleID, string selectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _jobTitleProvider.ListMajorUnselected(JobTitleID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpPost]
        [Route("ListMajorSelected/{JobTitleID}/{unselectedID}")]
        public ActionResult ListMajorSelected([FromBody] CustomDataSourceRequest request, int JobTitleID, string unselectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(unselectedID);
                var result = _jobTitleProvider.ListMajorSelected(JobTitleID, selectedIDDes);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = result.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpGet]
        [Route("GetLineOfBusiness")]
        public IActionResult GetLineOfBusiness()
        {
            var data = _jobTitleProvider.GetLineOfBusiness();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetJobFunction")]
        public IActionResult GetJobFunction()
        {
            var data = _jobTitleProvider.GetJobFunction();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpGet]
        [Route("GetLevelEdu")]
        public IActionResult GetLevelEdu()
        {
            var data = _jobTitleProvider.GetLevelEdu();
            return Ok(data.Select(x => new { Value = x.Id, Text = x.Name }));
        }

        [HttpPost]
        [Route("AddSpecification")]
        public IActionResult AddSpecification([FromBody] CreateEditSpecificationViewModel jobSpecification)
        {
            if (jobSpecification is null)
            {
                return BadRequest("Job Title is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            JobtitleSpecification entity = new JobtitleSpecification();
            _mapper.Map(jobSpecification, entity);
            _jobTitleProvider.AddSpecification(entity);
            if (entity.Id > 0)
            {
                _jobTitleProvider.UpdateListJobTitleSpecificationMajor(entity.Id, jobSpecification.Selected);
            }
            return Ok(jobSpecification);
        }

        [HttpPost]
        [Route("EditSpecification")]
        public IActionResult EditSpecification([FromBody] CreateEditSpecificationViewModel jobSpecification)
        {
            if (jobSpecification is null)
            {
                return BadRequest("Job Title is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var ID = Convert.ToInt32(jobSpecification.ID);
            //jobSpecification.ID = decryptedID.ToString();

            //var decryptedTitleID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(jobSpecification.JobTitleID));
            //jobSpecification.JobTitleID = decryptedTitleID.ToString();
            var entity = _jobTitleProvider.GetSpesification(ID);
            _mapper.Map(jobSpecification, entity);
            _jobTitleProvider.EditSpecification(entity);
            if (entity.Id > 0)
            {
                _jobTitleProvider.UpdateListJobTitleSpecificationMajor(entity.Id, jobSpecification.Selected);
            }
            return Ok(jobSpecification);
        }

        [HttpDelete]
        [Route("DeleteSpecification/{arrayOfID}")]
        public IActionResult DeleteSpecification(string arrayOfID)
        {
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                //foreach (var ID in arrayOfID)
                //{
                //    if (_jobTitleProvider.EditSpecification(ID))
                //    {
                //        return BadRequest("Unable to delete! Selected data has been used on another menu");
                //    }
                //}
                Array.ForEach(listID, _jobTitleProvider.DeleteSpecification);
            }
            catch (Exception)
            {
                throw;
            }
            return Ok();
        }

        [HttpPost]
        [Route("SpecificationValidateCombination")]
        public IActionResult SpecificationValidateCombination([FromBody]CreateEditSpecificationViewModel model)
        {
            JobtitleSpecification entity = new JobtitleSpecification();
            _mapper.Map(model, entity);
            var isValid = _jobTitleProvider.SpecificationValidateCombination(entity);
            return Ok(isValid);
        }

        #endregion JobSpesification

    }
}