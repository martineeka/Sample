using System;
using System.Linq;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobStatus;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class JobStatusController : ControllerBase
    {
        private IJobStatusService _jobStatusProvider;
        private IMapper _mapper;

        public JobStatusController(IJobStatusService jobStatusService, IMapper _mapper)
        {
            this._jobStatusProvider = jobStatusService;
            this._mapper = _mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                DataClaim.GetClaim(Request);
                var jobStatus = _jobStatusProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = jobStatus.ToDataSourceResult(req);

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
            var level = _jobStatusProvider.Get(Id);
            if (level == null)
            {
                return NotFound("Level not found.");
            }

            model.ID = Id.ToString();
            var levelMapper = _mapper.Map(level, model);
            return Ok(levelMapper);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            var level = new JobStatus();
            if (model is null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _mapper.Map(model, level);
            _jobStatusProvider.Add(level);
            return Ok(level);
        }

        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var level = _jobStatusProvider.Get(Convert.ToInt32(model.ID));
            var levelMapper = _mapper.Map(model, level);
            _jobStatusProvider.Edit(levelMapper);
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
                Array.ForEach(listID, _jobStatusProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("ValidateJobStatusCode/{code}/{id}")]
        public IActionResult ValidateJobStatusCode(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _jobStatusProvider.IsJobStatusCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _jobStatusProvider.ValidateCombination(model);
            return Ok(isValid);
        }
    }
}