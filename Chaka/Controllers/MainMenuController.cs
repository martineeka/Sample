using System;
using Chaka.Providers.SystemAdmin;
using Chaka.Utilities;
using Chaka.ViewModels.SystemAdmin.Menu;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.Controllers
{
    public class MainMenuController : Controller
    {
        string url = ApiUrl.SecurityUrl + "Login";
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MainMenuController(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }


        public IActionResult Index(string token)
        {
            Set("token", token, 600);
            DataClaim.Token = token;
            return View();
        }

        public void Set(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
            {
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            }
            else
            {
                option.Expires = DateTime.Now.AddMilliseconds(10);
            }
            Response.Cookies.Append(key, value, option);
        }



    }
}