using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers;
using Chaka.ViewModels.SysAdmin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.API.Areas.SystemAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserProvider _userProvider;
        private IMapper _mapper;

        public UserController(UserProvider userProvider, IMapper mapper)
        {
            _userProvider = userProvider;
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var users = _userProvider.List();
            var userDtosViewModel = _mapper.Map<IList<ListUserViewModel>>(users);
            return Ok(userDtosViewModel);
        }

       
        [HttpPost("Create")]
        public IActionResult Create([FromBody]ListUserViewModel userDto)
        {
            // map dto to entity
            var user = _mapper.Map<ListUserViewModel>(userDto);

            try
            {
                // save 
                _userProvider.AddUser(user);
                return Ok(user.ID);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}