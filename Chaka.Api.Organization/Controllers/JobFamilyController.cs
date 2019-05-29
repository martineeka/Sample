using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobFamily;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class JobFamilyController : ControllerBase
    {
        private readonly IJobFamilyService _JobFamilyProvider;
        private IMapper _mapper;

        public JobFamilyController(IJobFamilyService JobFamilyProvider, IMapper mapper)
        {
            _JobFamilyProvider = JobFamilyProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _JobFamilyProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = levels.ToDataSourceResult(req);

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
            var model = new CreateEditViewModel();
            var jobfamily = _JobFamilyProvider.Get(Id);
            if (jobfamily == null)
            {
                return NotFound("Level not found.");
            }

            model.ID = Id.ToString();
            _mapper.Map(jobfamily, model);
            return Ok(jobfamily);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            var jobfamily = new JobFamily();
            if (model is null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, jobfamily);
            _JobFamilyProvider.Add(jobfamily);
            return Ok(jobfamily);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var jobfamily = _JobFamilyProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, jobfamily);
            _JobFamilyProvider.Edit(jobfamily);
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
                    var ValidateDelete = _JobFamilyProvider.ValidateDelete(ID);
                    if (ValidateDelete.IsSuccess)
                    {
                        model.IsSuccess = false;
                        model.Message = "Unable to delete! Selected data has been used on "+ ValidateDelete.Message + " menu";

                        return BadRequest(model);
                    }
                }
                Array.ForEach(listID, _JobFamilyProvider.Delete);
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
        [Route("ValidateJobFamilyCode/{code}/{id}")]
        public IActionResult ValidateJobFamilyCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _JobFamilyProvider.IsJobFamilyCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpGet]
        [Route("ValidateJobFamilyCodeExist/{code}")]
        public IActionResult ValidateJobFamilyCodeExist(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _JobFamilyProvider.IsJobFamilyCodeExist(code);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _JobFamilyProvider.validateCombination(model);
            return Ok(isValid);
        }




        #region Filter 
        [HttpPost]
        [Route("GetMajorList/{id}")]
        public IActionResult GetDescriptionList([FromBody]CustomDataSourceRequest request, string id)
        {
            try
            {
                var ID = EncryptionHelper.DecryptUrlParam(id);
                var jobTitle = _JobFamilyProvider.GetMajorList(Convert.ToInt32(ID));
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
        [Route("GetDetail/{encryptedJobFamilyID}")]
        public IActionResult Index(string encryptedJobFamilyID)
        {
            var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(encryptedJobFamilyID));
            var jobFamily = _JobFamilyProvider.Get(decryptID);

            var viewModel = new IndexDetailViewModel();
            viewModel.JobFamilyID = decryptID;
            viewModel.Code = jobFamily.Code;
            viewModel.Name = jobFamily.Name;
            viewModel.Description = jobFamily.Description;

            int[] IDs = null;
            IDs = _JobFamilyProvider.GetJobFamilyID(decryptID);

            viewModel.List = _JobFamilyProvider.ListUnselected(decryptID, IDs);
            if (viewModel.List == null)
                viewModel.List = new List<ListDetailViewModel>();

            IDs = viewModel.List.Select(s => s.ID).ToArray();
            viewModel.ListSelected = _JobFamilyProvider.ListSelected(decryptID, IDs);
            if (viewModel.ListSelected == null)
                viewModel.ListSelected = new List<ListDetailViewModel>();

            return Ok(viewModel);
        }

        [HttpPost]
        [Route("ListUnselected/{jobFamilyID}/{selectedID}")]
        public ActionResult ListUnselected([FromBody] CustomDataSourceRequest request, int jobFamilyID, string selectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(selectedID);
                var result = _JobFamilyProvider.ListUnselected(jobFamilyID, selectedIDDes);
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
        [Route("ListSelected/{jobFamilyID}/{unselectedID}")]
        public ActionResult ListSelected([FromBody] CustomDataSourceRequest request, int jobFamilyID, string unselectedID)
        {
            try
            {
                int[] selectedIDDes = JsonConvert.DeserializeObject<int[]>(unselectedID);
                var result = _JobFamilyProvider.ListSelected(jobFamilyID, selectedIDDes);
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
        [Route("UpdateDetail")]
        public ActionResult UpdateDetail([FromBody]IndexDetailViewModel model)
        {
            try
            {
                _JobFamilyProvider.UpdateListJobFamily(model.JobFamilyID, model.Selected);
                return Ok();
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
                return BadRequest(ajaxViewModel);
            }
        }
        #endregion
    }
}