using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobMaster;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]

    public class JobMasterController : ControllerBase
    {
        private readonly IJobMasterService _jobMasterProvider;
        private IMapper _mapper;


        public JobMasterController(IJobMasterService jobMasterProvider,
                              IMapper mapper)
        {
            _jobMasterProvider = jobMasterProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var jobMasters = _jobMasterProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = jobMasters.ToDataSourceResult(req);

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
            var jobMasters = _jobMasterProvider.Get(Id);
            if (jobMasters == null)
            {
                return NotFound("Job Master not found.");
            }

            return Ok(jobMasters);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel jobMaster)
        {
            if (jobMaster is null)
            {
                return BadRequest("Job Master is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var jobMasterMapper = _mapper.Map<Database.Context.Models.JobMaster>(jobMaster);
            _jobMasterProvider.Add(jobMasterMapper);
            return Ok(jobMaster);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel jobMaster)
        {
            if (jobMaster == null)
            {
                return BadRequest("Job Master is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptID = EncryptionHelper.DecryptUrlParam(jobMaster.ID);
            jobMaster.ID = decryptID;
            var jobmasters = _jobMasterProvider.Get(Convert.ToInt32(decryptID));
            var jobMasterMapper = _mapper.Map(jobMaster, jobmasters);
            //jobMasterMapper.Id = jobmasters.Id;
            //jobMasterMapper.CreatedDate = jobmasters.CreatedDate;
            //jobMasterMapper.CreatedBy = jobmasters.CreatedBy;
            _jobMasterProvider.Edit(jobMasterMapper);
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
                Array.ForEach(listID, _jobMasterProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        // VALIDATE api/<controller>/5
        [HttpPost]
        [Route("Validate")]
        public IActionResult ValidateCombination([FromBody]CreateEditViewModel model)
        {
            model.ID = (model.ID == null || model.ID == "" ? "0" : EncryptionHelper.DecryptUrlParam(model.ID));
            var isValid = _jobMasterProvider.validateCombination(model);
            return Ok(isValid);
        }

       


        [HttpPost]
        [Route("ValidateCode/{name}/{id}")]
        public IActionResult ValidateCode(string code, string id)
        {
            try
            {
                bool isJobMasterValid = _jobMasterProvider.IsJobMasterCodeValid(code, id);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }
    }
}