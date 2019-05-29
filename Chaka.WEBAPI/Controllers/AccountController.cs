using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.SystemAdmin.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Chaka.WEBAPI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userProvider;
        private readonly AppSettings _appSettings;


        public AccountController(IUserService userProvider, IOptions<AppSettings> appSettings)
        {
            _userProvider = userProvider;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: api/<controller>
        [HttpPost]
        [Route("Login")]
        public ActionResult Login([FromBody] LoginViewModel model)
        {
            //LoginViewModel model = new LoginViewModel();
            //model.UserName = "fetra.fydhianto";
            //model.Password = "Pass123.";
            var user = _userProvider.ValidateLogin(model);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim("BusinessGrupID" , user.CurrentBusinessGroupId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                EmployeeID = user.EmployeeId,
                Username = user.UserName,
                Token = tokenString
            });
        }
    }
}