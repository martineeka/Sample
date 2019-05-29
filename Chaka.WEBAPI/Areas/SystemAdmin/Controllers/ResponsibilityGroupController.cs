using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.ViewModels.SystemAdmin.ResponsibilityGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Chaka.ViewModels.CustomModel;
using Chaka.Utilities;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Chaka.WEBAPI.Areas.SystemAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsibilityGroupController : ControllerBase
    {
        private readonly IResponsibilityGroupService _responibilityGroupProvider;
        private IMapper _mapper;

        public ResponsibilityGroupController(IResponsibilityGroupService responibilityGroupProvider, IMapper mapper)
        {
            _responibilityGroupProvider = responibilityGroupProvider;
            _mapper = mapper;
        }


        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var responsibility = _responibilityGroupProvider.GetList();

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
            var responsibilitygroup = _responibilityGroupProvider.Get(Id);
            if (responsibilitygroup == null)
            {
                return NotFound("Responsibility Group not found.");
            }

            return Ok(responsibilitygroup);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel resGroup)
        {
            if (resGroup is null)
            {
                return BadRequest("Responsibility Group is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var resGroupMapper = _mapper.Map<ResponsibilityGroup>(resGroup);
            _responibilityGroupProvider.Add(resGroupMapper);
            return Ok(resGroupMapper);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel resGroup)
        {
            if (resGroup == null)
            {
                return BadRequest("ResponsibilityGroup is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var resGroups = _responibilityGroupProvider.Get(Convert.ToInt32(resGroup.ID));
            var resGroupMapper = _mapper.Map<ResponsibilityGroup>(resGroup);
            resGroupMapper.Id = resGroups.Id;
            resGroupMapper.CreatedDate = resGroups.CreatedDate;
            resGroupMapper.CreatedBy = resGroups.CreatedBy;
            _responibilityGroupProvider.Edit(resGroupMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public IActionResult Delete(int id)
        {
            var responsibilitygroup = _responibilityGroupProvider.Get(id);
            if (responsibilitygroup == null)
            {
                return BadRequest("Responsibility Group not found.");
            }
            _responibilityGroupProvider.Delete(responsibilitygroup);
            return Ok();
        }
    }
}