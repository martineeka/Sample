using System;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.SystemAdmin.Account;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Controllers
{

    public class AccountController : Controller
    {
        string url = ApiUrl.SecurityUrl + "Login";

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var jsonViewModel = new AjaxLoginViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url;
                jsonViewModel = Utilities.RestAPIHelper<AjaxLoginViewModel>.Submit(json, Method.POST, endpoint);
                if (jsonViewModel.Data != null)
                    jsonViewModel.SetValuesLogin(true, jsonViewModel.BearerToken, jsonViewModel.Data, "Login");
                else
                    jsonViewModel.SetValuesLogin(false, jsonViewModel.BearerToken, jsonViewModel.Data, jsonViewModel.Message);




            }
            catch (Exception ex)
            {
                jsonViewModel.SetValuesLogin(false, "", null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        [HttpPost]
        public JsonResult UpdateCurrentBussinessGroupID(EditCurrentBussinessGroupUserViewModel model)
        {
            AjaxViewModel jsonViewModel = new AjaxViewModel();

            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = ApiUrl.SecurityUrl + "UpdateCurrentBussinessGroupID";
                Utilities.RestAPIHelper<EditCurrentBussinessGroupUserViewModel>
                                          .Submit(json, Method.POST, endpoint);
                jsonViewModel.SetValues(true, null, "CurrentBussinessGroupID has been updated");

            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }
    }
}