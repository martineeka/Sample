using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Browse;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.User;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class UserController : Controller
    {
        string url = ApiUrl.SecurityUrl + "User";
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List([DataSourceRequest] DataSourceRequest request)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + Route.Get;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListUserViewModel>>
                                      .Submit(json, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

                foreach (var item in result.data)
                {
                    item.ID = EncryptionHelper.EncryptUrlParam(item.ID);
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
                return Json(jsonViewModel);
            }
            return Json(newDataSourceResult);
        }

        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            model.action = "create";
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            bool IsValidEmail;
            try
            {
                var addr = new System.Net.Mail.MailAddress(model.Email);
                model.Email = addr.ToString();
                IsValidEmail = true;
            }
            catch
            {
                IsValidEmail = false;
            }

            var Password = model.Password;
            int Number = 0;
            int Text = 0;
            for( int i = 0; i < Password.Length; i++ )
            {
                int value;
                string Character = Password.Substring(i, 1);
                if (int.TryParse(Character, out value))
                    Number++;
                else
                    Text++;
            }

            if (Password.Length < 8 || Number < 2 || !IsValidEmail || Text == 0)
            {
                if(!IsValidEmail)
                    jsonViewModel.SetValues(false, null, String.Format("Email is invalid"));
                else
                    jsonViewModel.SetValues(false, null, String.Format("Password must be at least 8 characters with minimun 2 number"));
            }
            else
            {
                try
                {
                    model.Password = RijndaelHelper.Encrypt(model.Password, "IGLO2015");
                    model.LoginName = model.UserName;
                    model.EmployeeID = EncryptionHelper.DecryptUrlParam(model.EmployeeID);
                    string json = JsonConvert.SerializeObject(model);
                    var checkCode = Utilities.RestAPIHelper<string>
                                      .Submit(json, Method.POST, url + "/UserValidateCombination");
                    if (!Convert.ToBoolean(checkCode))
                    {
                        var endpoint = url + Route.Add;
                        var content = Utilities.RestAPIHelper<CreateEditViewModel>
                                                  .Submit(json, Method.POST, endpoint);
                        jsonViewModel.SetValues(true, null, "Saved");
                    }
                    else
                    {
                        jsonViewModel.SetValues(false, null, String.Format("User has been added"));
                    }
                }
                catch (Exception ex)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
                }
            }

            
            return Json(jsonViewModel);
        }

        public ActionResult Edit(string id)
        {
            var jsonViewModel = new AjaxViewModel();

            CreateEditViewModel model = new CreateEditViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Get + "/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditViewModel>
                                            .Submit("", Method.GET, endpoint);
                model.action = "update";
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Edit(CreateEditViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            bool IsValidEmail;
            try
            {
                var addr = new System.Net.Mail.MailAddress(model.Email);
                model.Email = addr.ToString();
                IsValidEmail = true;
            }
            catch
            {
                IsValidEmail = false;
            }

            if (!IsValidEmail)
            {
                jsonViewModel.SetValues(false, null, String.Format("Email is invalid"));
            }
            else
            {
                try
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();
                    model.EmployeeID = EncryptionHelper.DecryptUrlParam(model.EmployeeID);
                    string json = JsonConvert.SerializeObject(model);
                    var checkCode = Utilities.RestAPIHelper<string>
                                      .Submit(json, Method.POST, url + "/UserValidateCombination");
                    if (!Convert.ToBoolean(checkCode))
                    {
                        var endpoint = url + Route.Edit;
                        model = Utilities.RestAPIHelper<CreateEditViewModel>
                                                .Submit(json, Method.PUT, endpoint);
                        jsonViewModel.SetValues(true, null, "Saved");
                    }
                    else
                    {
                        jsonViewModel.SetValues(false, null, String.Format("User has been added"));
                    }

                }
                catch (Exception ex)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
                }
            }

            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult Delete(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + Route.Delete + "/" + json;
                Utilities.RestAPIHelper<string>
                                      .Submit("", Method.POST, endpoint);
                ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public JsonResult GetEmployee()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetEmployee";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetEmployeeListFilter()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetEmployeeListFilter";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetResponsibilityGroup()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetResponsibilityGroup";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetPreferenceGroup()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetPreferenceGroup";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetRestrictionGroup()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetRestrictionGroup";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetUserStatus()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetUserStatus";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public IActionResult IndexEmployee(string callBack)
        {
            var model = new IndexBrowseEmployeeViewModel();
            model.Callback = callBack;
            return PartialView("IndexEmployee", model);
        }

        [HttpPost]
        public ActionResult ListEmployee([DataSourceRequest] DataSourceRequest request)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetListEmployee";

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<BrowseEmployeeViewModel>>
                                      .Submit(json, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }
    }
}