using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.Api.Security.Areas.SystemAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuDetailController : ControllerBase
    {
        IMenuDetailService _menuDetailProvider;
        IMapper _mapper;
        public MenuDetailController(IMenuDetailService provider, IMapper mapper)
        {
            this._menuDetailProvider = provider;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Add/{menuID}")]
        public IActionResult Add([FromBody]CreateMenuDetailViewModel model, int menuID)
        {
            DataClaim.GetClaim(Request);
            if (model is null)
            {
                return BadRequest("Menu is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newMenuDetail = new MenuDetail();
            newMenuDetail.MenuId = menuID;
            _mapper.Map(model, newMenuDetail);
            _menuDetailProvider.Add(newMenuDetail);
            return Ok(newMenuDetail);
        }
    }
}