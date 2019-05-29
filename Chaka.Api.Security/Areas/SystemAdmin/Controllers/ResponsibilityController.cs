using System;
using System.Linq;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.Responsibility;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    //[Authorize]
    [Route("api/Security/[controller]")]
    [ApiController]
    public class ResponsibilityController : ControllerBase
    {
        private readonly IMenuService _menuProvider;
        private readonly IResponsibilityService _responibilityProvider;
        private readonly IOrgUnitService _organizationUnitProvider;
        private IMapper _mapper;


        public ResponsibilityController(IMenuService menuProvider,
                                        IResponsibilityService responibilityProvider,
                                        IOrgUnitService organizationUnitProvider,
                                        IMapper mapper)
        {
            _menuProvider = menuProvider;
            _responibilityProvider = responibilityProvider;
            _organizationUnitProvider = organizationUnitProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var responsibility = _responibilityProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = responsibility.ToDataSourceResult(req);

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
            var responsibility = _responibilityProvider.Get(Id);
            if (responsibility == null)
            {
                return NotFound("Menu not found.");
            }

            return Ok(responsibility);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel responsiblity)
        {
            if (responsiblity is null)
            {
                return BadRequest("Responsiblity is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var resMapper = _mapper.Map<Responsibility>(responsiblity);
            _responibilityProvider.Add(resMapper);
            return Ok(responsiblity);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel responsiblity)
        {
            if (responsiblity == null)
            {
                return BadRequest("Responibility is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = _responibilityProvider.Get(Convert.ToInt32(responsiblity.ID));
            var resMapper = _mapper.Map<Responsibility>(responsiblity);
            resMapper.Id = res.Id;
            resMapper.CreatedDate = res.CreatedDate;
            resMapper.CreatedBy = res.CreatedBy;
            _responibilityProvider.Edit(resMapper);
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
                foreach (var ID in listID)
                {
                    var IsUsed = _responibilityProvider.IsUsedByRespGroupDetail(ID);
                    if (IsUsed)
                        return Ok(IsUsed);
                }
                Array.ForEach(listID, _responibilityProvider.Delete);
            }
            catch (Exception)
            {

                throw;
            }
            return Ok();
        }

        [HttpGet]
        [Route("GetMenu")]
        public IActionResult GetMenus()
        {
            var data = _menuProvider.GetList();
            return Ok(data.Select(x => new { Value = x.ID, Text = x.NameENG }));
        }

        [HttpGet]
        [Route("GetBussinesGroup")]
        public IActionResult GetBusinessGroup()
        {
            var data = _organizationUnitProvider.GetList();
            return Ok(data.Select(x => new { Value = x.ID, Text = x.Name }));
        }
    }
}