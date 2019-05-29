using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.CostCenter;
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
    public class CostCenterController : ControllerBase
    {
        private readonly ICostCenterService _costCenterProvider;
        private IMapper _mapper;

        public CostCenterController(ICostCenterService costCenterProvider, IMapper mapper)
        {
            _costCenterProvider = costCenterProvider;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                DataClaim.GetClaim(Request);
                var CostCenter = _costCenterProvider.GetList();
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
            var CostCenter = _costCenterProvider.Get(Id);
            if (CostCenter == null)
            {
                return NotFound("Cost Center not found.");
            }

            return Ok(CostCenter);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel CostCenter)
        {
            DataClaim.GetClaim(Request);
            var model = new CostCenter();
            if (CostCenter is null)
            {
                return BadRequest("Cost Center is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var CostCenterMapper = _mapper.Map(CostCenter, model);
            _costCenterProvider.Add(CostCenterMapper);
            return Ok(CostCenter);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel model)
        {
            DataClaim.GetClaim(Request);
            if (model == null)
            {
                return BadRequest("Cost Center is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var CostCenter = _costCenterProvider.Get(Convert.ToInt32(model.ID));
            var CostCenterMapper = _mapper.Map(model, CostCenter);
            _costCenterProvider.Edit(CostCenterMapper);
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
                    var IsUsed = _costCenterProvider.IsUsedByOrgUnit(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _costCenterProvider.Delete);
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
            var isValid = _costCenterProvider.validateCombination(model);
            return Ok(isValid);
        }
    }
}