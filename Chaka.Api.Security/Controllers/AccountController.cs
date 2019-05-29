using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.SystemAdmin.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Chaka.Api.Security.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userProvider;
        private readonly AppSettings _appSettings;
        private IMapper _mapper;



        public AccountController(IUserService userProvider,
                                IOptions<AppSettings> appSettings,
                                IMapper mapper)
        {
            _userProvider = userProvider;
            _appSettings = appSettings.Value;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: api/<controller>
        [HttpPost]
        [Route("api/Security/Login")]
        public ActionResult Login([FromBody] LoginViewModel model)
        {
            var user = _userProvider.ValidateLogin(model);

            if (user == null)
                return BadRequest(new { IsSuccess = false, Data = "", BearerToken = "", message = "Username or password is incorrect" });


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("CurrentUserID", user.Id.ToString()),
                    new Claim("CurrentBusinessGrupID" , user.CurrentBusinessGroupId.ToString()),
                    new Claim("CurrentEmail" , user.Email.ToString()),
                    new Claim("LoginName" , model.LoginName),
                    new Claim("Password" ,  RijndaelHelper.Encrypt(model.Password,"IGLO2015"))
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //var menuMapper = _mapper.Map<Database.Context.Models.Menu>(menu);
            var returnLogin = _mapper.Map<User>(user);
            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                IsSuccess = true,
                Data = returnLogin,
                BearerToken = tokenString,
                message = "Login Success"
            });
        }

        [HttpPost]
        [Route("api/Security/UpdateCurrentBussinessGroupID")]
        public ActionResult UpdateCurrentBussinessGroupID([FromBody] EditCurrentBussinessGroupUserViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Job Master is null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int CurrentBussinessGroupID = Convert.ToInt32(model.CurrentBussinessGroupID);
            int UserID = Convert.ToInt32(model.ID);
            _userProvider.EditCurrentBussinessGroupID(CurrentBussinessGroupID, UserID);
            return Ok();
        }
    }
}