using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chaka.ViewModels.Organization.SIUPClass;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Chaka.Database.Context.Models;
using Newtonsoft.Json;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class SIUPClassController : ControllerBase
    {
        private readonly ISIUPClassService _SIUPClassProvider;
        private IMapper _mapper;

        public SIUPClassController(ISIUPClassService SIUPClassProvider, IMapper mapper)
        {
            _SIUPClassProvider = SIUPClassProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                DataClaim.GetClaim(Request);
                var CostCenter = _SIUPClassProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = CostCenter.ToDataSourceResult(req);

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
            DataClaim.GetClaim(Request);
            var SIUP = _SIUPClassProvider.Get(Id);
            if (SIUP == null)
            {
                return NotFound("SIUP not found.");
            }

            return Ok(SIUP);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel Siup)
        {
            DataClaim.GetClaim(Request);
            var model = new Siupclass();
            if (Siup is null)
            {
                return BadRequest("SIUP Class is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var SiupMapper = _mapper.Map(Siup, model);
            _SIUPClassProvider.Add(SiupMapper);
            return Ok(Siup);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("SIUP is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var SIUP = _SIUPClassProvider.Get(Convert.ToInt32(model.ID));
            var SIUPMapper = _mapper.Map(model, SIUP);
            _SIUPClassProvider.Edit(SIUPMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpPost]
        [Route("Delete/{arrayOfID}")]
        public IActionResult Delete(string arrayOfID)
        {
            DataClaim.GetClaim(Request);
            string[] DeserializeID = JsonConvert.DeserializeObject<string[]>(arrayOfID);
            int[] listID = DeserializeID.Select(s => Convert.ToInt32(EncryptionHelper.DecryptUrlParam(s))).ToArray();
            try
            {
                foreach (var ID in listID)
                {
                    var IsUsed = _SIUPClassProvider.IsUsedByLegalEntity(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _SIUPClassProvider.Delete);
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
            var isValid = _SIUPClassProvider.validateCombination(model);
            return Ok(isValid);
        }
    }
}