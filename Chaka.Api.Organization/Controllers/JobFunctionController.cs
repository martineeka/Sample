using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobFunction;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class JobFunctionController : ControllerBase
    {
        private IJobFunctionServices _JobFunctionProvider;
        private IMapper _mapper;

        public JobFunctionController(IJobFunctionServices JobFunctionProvider, IMapper mapper)
        {
            _JobFunctionProvider = JobFunctionProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var levels = _JobFunctionProvider.GetList();

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
            var data = _JobFunctionProvider.Get(Id);
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
            var data = new JobFunction();
            if (model is null)
            {
                return BadRequest("Data is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _mapper.Map(model, data);
            _JobFunctionProvider.Add(data);
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
            var data = _JobFunctionProvider.Get(Convert.ToInt32(model.ID));
            _mapper.Map(model, data);
            _JobFunctionProvider.Edit(data);
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
                Array.ForEach(listID, _JobFunctionProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }


        [HttpGet]
        [Route("IsCodeValid/{code}/{id}")]
        public IActionResult IsCodeValid(string code, string id)
        {
            var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _JobFunctionProvider.IsCodeValid(code, decryptID);
            return Ok(isLevelCodeValid);
        }

        [HttpGet]
        [Route("IsCodeExist/{code}")]
        public IActionResult IsCodeExist(string code)
        {
            //var decryptID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var isLevelCodeValid = _JobFunctionProvider.IsCodeExist(code);
            return Ok(isLevelCodeValid);
        }
    }
}