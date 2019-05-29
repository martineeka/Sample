using System;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using Chaka.ViewModels.SystemAdmin.Menu;
using Newtonsoft.Json;
using RestSharp;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.Helpers;
using Chaka.ViewModels.CustomModel;
using System.Linq;

namespace Chaka.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class MenuController : Controller
    {
        string url = ApiUrl.SecurityUrl + "Menu";
        string menuDetailUrl = ApiUrl.SecurityUrl + "MenuDetail";

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexFunction(string menuID)
        {
            var model = new ListMenuViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(menuID));

            var endpoint = url + "/Get/" + decryptID;
            var menu = Utilities.RestAPIHelper<ListMenuViewModel>.Submit("", Method.GET, endpoint);

            model.ID = menuID;
            model.NameINA = menu.NameINA;
            model.NameENG = menu.NameENG;
            model.Description = menu.Description;

            return View(model);

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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListMenuViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

                foreach (var item in result.data)
                {
                    item.ID = EncryptionHelper.EncryptUrlParam(item.ID);
                    item.Description = item.Description == null ? item.Description = "N/A" : item.Description;
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        public JsonResult ListFunction([DataSourceRequest]DataSourceRequest request, string menuID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(menuID));
                var endpoint = url + "/GetListFunction/" + decryptID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListMenuFunctionViewModel>>
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



        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + Route.Add;
                var content = Utilities.RestAPIHelper<CreateEditViewModel>
                                          .Submit(json, Method.POST, endpoint, Request);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult CreateMenuDetail(string headerID)
        {
            var jsonViewModel = new AjaxViewModel();
            IndexDetailViewModel model = new IndexDetailViewModel();
            try
            {
                var endpoint = String.Format("{0}{1}/{2}", url, Route.GetDetail, headerID);
                model = Utilities.RestAPIHelper<IndexDetailViewModel>
                                            .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateMenuDetail", model);


        }

        [HttpPost]
        public ActionResult CreateMenuDetail(CreateMenuDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var decryptId = Convert.ToInt32(Utilities.EncryptionHelper.DecryptUrlParam(model.MenuID));
                string json = JsonConvert.SerializeObject(model);
                var endpoint = menuDetailUrl + "/Add/" + decryptId;
                var content = Utilities.RestAPIHelper<CreateMenuDetailViewModel>.Submit(json, Method.POST, endpoint, Request);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
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
            try
            {
                var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                model.ID = decryptedID.ToString();

                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + Route.Edit;
                model = Utilities.RestAPIHelper<CreateEditViewModel>
                                        .Submit(json, Method.PUT, endpoint, Request);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
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

        public ActionResult ValidateMenuNameIna(string name, string id)
        {
            bool isMenuNameValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Validate + "/" + name + "/" + decryptedID;
                isMenuNameValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isMenuNameValid);
        }

        public ActionResult ValidateMenuNameEng(string name, string id)
        {
            bool isMenuNameValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Validate + "/" + name + "/" + decryptedID;
                isMenuNameValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isMenuNameValid);
        }

        public JsonResult ListUnselected([DataSourceRequest] DataSourceRequest request, int menuID, string[] selectedID)
        {
            int[] IDs = null;
            if (selectedID == null)
                IDs = null;
            else
                IDs = selectedID.Select(s => Convert.ToInt32(s)).ToArray();

            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                string json = JsonConvert.SerializeObject(selectedID);
                string jsonBody = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                //potong bagian akhir, termasuk UnselectedID:null}
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 25);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListUnselected", menuID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListMenuFunctionViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }


        public JsonResult ListSelected([DataSourceRequest] DataSourceRequest request, int menuID, string[] unselectedID)
        {
            int[] IDs = null;
            if (unselectedID == null)
                IDs = null;
            else
                IDs = unselectedID.Select(s => Convert.ToInt32(s)).ToArray();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                string json = JsonConvert.SerializeObject(unselectedID);
                string jsonBody = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                //string jsonTemp = System.Text.RegularExpressions.Regex.Replace(jsonBody, "null}", "");
                //potong bagian akhir, null}
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListSelected", menuID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListMenuFunctionViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        [HttpPost]
        public ActionResult UpdateDetail(IndexDetailViewModel model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/{1}", url, "UpdateDetail");
                var result = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit(json, Method.POST, endpoint, Request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessadddge : {0}", ex.GetBaseException().Message));
                return Json(ajaxViewModel);
            }
        }



    }
}