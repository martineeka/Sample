using System;
using System.Collections.Generic;
using System.Linq;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc;
using Chaka.ViewModels.SystemAdmin.Menu;
using AutoMapper;
using Chaka.Utilities;

namespace Chaka.WEBAPI.Areas.SystemAdmin.Controllers
{
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuProvider;
        private IMapper _mapper;


        public MenuController(IMenuService menuProvider,
                              IMapper mapper)
        {
            _menuProvider = menuProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var menus = _menuProvider.GetList();

                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);

                var filter = menus.ToDataSourceResult(req);

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
            var menus = _menuProvider.Get(Id);
            if (menus == null)
            {
                return NotFound("Menu not found.");
            }

            return Ok(menus);
        }

        // POST api/<controller>
        [HttpPost]
        [Route("Add")]
        public IActionResult Post([FromBody] CreateEditViewModel menu)
        {
            if (menu is null)
            {
                return BadRequest("Menu is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var menuMapper = _mapper.Map<Database.Context.Models.Menu>(menu);
            _menuProvider.Add(menuMapper);
            return Ok(menu);
        }

        // PUT api/<controller>/5
        [HttpPut]
        [Route("Edit")]
        public IActionResult Put([FromBody] CreateEditViewModel menu)
        {
            if (menu == null)
            {
                return BadRequest("Menu is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var menus = _menuProvider.Get(Convert.ToInt32(menu.ID));
            var menuMapper = _mapper.Map<Database.Context.Models.Menu>(menu);
            menuMapper.Id = menus.Id;
            menuMapper.CreatedDate = menus.CreatedDate;
            menuMapper.CreatedBy = menus.CreatedBy;
            _menuProvider.Edit(menuMapper);
            return Ok();
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        [Route("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var menu = _menuProvider.Get(Id);
            if (menu == null)
            {
                return BadRequest("Menu not found.");
            }
            _menuProvider.Delete(menu);
            return Ok();
        }

        //[HttpGet("Detail/{id}", Name = "Detail")]
        [HttpGet]
        [Route("MenuDetail/{Id}")]
        public IActionResult MenuDetail(int Id)
        {
            var menuDetail = _menuProvider.GetMenuDetailViewModel(Id);
            return Ok(menuDetail);
        }
    }
}
