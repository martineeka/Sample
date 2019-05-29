using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Chaka.ViewModels.Organization.OrganizationLevel;
using Chaka.Database.Context.Models;
using Microsoft.AspNetCore.Authorization;

namespace Chaka.Api.Organization.Controllers
{
    [Authorize]
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class OrganizationLevelController : ControllerBase
    {
        private readonly IOrganizationLevelService _orgLevelProvider;
        private IMapper _mapper;

        public OrganizationLevelController(IOrganizationLevelService orgLevelProvider, IMapper mapper)
        {
            _orgLevelProvider = orgLevelProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                DataClaim.GetClaim(Request);
                var OrgLevel = _orgLevelProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = OrgLevel.ToDataSourceResult(req);

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
            var OrgLevel = _orgLevelProvider.Get(Id);
            if (OrgLevel == null)
            {
                return NotFound("Organization Level not found.");
            }

            return Ok(OrgLevel);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel orgLevel)
        {
            DataClaim.GetClaim(Request);
            var model = new OrganizationLevel();
            if (orgLevel is null)
            {
                return BadRequest("Group Grade is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var orgLevelMapper = _mapper.Map(orgLevel, model);
            _orgLevelProvider.Add(orgLevelMapper);
            return Ok(orgLevel);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Organization Level is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var orgLevel = _orgLevelProvider.Get(Convert.ToInt32(model.ID));
            var orgLevelMapper = _mapper.Map(model, orgLevel);
            _orgLevelProvider.Edit(orgLevelMapper);
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
                    var IsUsed = _orgLevelProvider.IsUsedByOrgUnit(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _orgLevelProvider.Delete);
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
            var isValid = _orgLevelProvider.validateCombination(model);
            return Ok(isValid);
        }
    }
}