using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.Country;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class CountryController : Controller
    {
        string url = ApiUrl.OrganizationUrl + "Country";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexProvince(string countryID)
        {
            var model = new IndexProvinceViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(countryID));

            var endpoint = url + "/Get/" + decryptID;
            var country = Utilities.RestAPIHelper<ListViewModel>.Submit("", Method.GET, endpoint);

            model.ID = countryID;
            model.CountryName  = country.Name;
            model.CountryCode = country.Code;
            model.CountryDescription = country.Description;

            return View(model);

        }

        public IActionResult IndexCity(string provinceID)
        {
            var model = new IndexCityViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(provinceID));

            var endpoint = url + "/GetIndexCity/" + decryptID;
            var province = Utilities.RestAPIHelper<IndexCityViewModel>.Submit("", Method.GET, endpoint);

            //model.ID = provinceID;
            model.ProvinceID = provinceID;
            model.CountryName = province.CountryName;
            model.CountryCode = province.CountryCode;
            model.ProvinceName = province.ProvinceName;

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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListViewModel>>
                                      .Submit(json, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

                foreach (var item in result.data)
                {
                    item.ID = EncryptionHelper.EncryptUrlParam(item.ID);
                    //item.Description = item.Description == null ? item.Description = "N/A" : item.Description;
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        public JsonResult ListProvince([DataSourceRequest]DataSourceRequest request, string countryID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(countryID));
                var endpoint = url + "/GetListProvince/" + decryptID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListProvinceViewModel>>
                                      .Submit(json, Method.POST, endpoint);

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

        public JsonResult ListCity([DataSourceRequest]DataSourceRequest request, string provinceID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(provinceID));
                var endpoint = url + "/GetListCity/" + decryptID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListCityViewModel>>
                                      .Submit(json, Method.POST, endpoint);

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
                var endpoint = String.Format("{0}{1}", url, Route.Validate);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}{1}", url, Route.Add);
                    var content = Utilities.RestAPIHelper<CreateEditViewModel>
                                              .Submit(json, Method.POST, endpoint, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult CreateProvince(string headerID)
        {
            var model = new CreateEditProvinceViewModel();
            model.CountryID = headerID;
            return PartialView("CreateEditProvince", model);
        }

        [HttpPost]
        public ActionResult CreateProvince(CreateEditProvinceViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {

                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateProvince";
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.CountryID));
                    endpoint = url + "/AddProvince/" + decryptID;
                    var content = Utilities.RestAPIHelper<CreateEditProvinceViewModel>
                                              .Submit(json, Method.POST, endpoint, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult CreateCity(string headerID)
        {
            var model = new CreateEditCityViewModel();
            model.ProvinceID = headerID;
            return PartialView("CreateEditCity", model);
        }

        [HttpPost]
        public ActionResult CreateCity(CreateEditCityViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {

                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateCity";
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    var decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ProvinceID));
                    endpoint = url + "/AddCity/" + decryptID;
                    var content = Utilities.RestAPIHelper<CreateEditCityViewModel>
                                              .Submit(json, Method.POST, endpoint, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }
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
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, url + "/ValidateCode/" + model.Code + "/" + model.ID);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Country Code has been used"));
                }
                else
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();

                    string json = JsonConvert.SerializeObject(model);
                    var endpoint = url + Route.Edit;
                    model = Utilities.RestAPIHelper<CreateEditViewModel>
                                            .Submit(json, Method.PUT, endpoint, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }

            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditProvince(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditProvinceViewModel model = new CreateEditProvinceViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetProvince/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditProvinceViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditProvince", model);
        }

        [HttpPost]
        public ActionResult EditProvince(CreateEditProvinceViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var endpoint = url + "/ValidateProvinceCode/" + model.Code + "/" + model.ID;
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, endpoint);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Province Code has been used"));
                }
                else
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();

                    string json = JsonConvert.SerializeObject(model);
                    var endpoint2 = url + "/EditProvince";
                    model = Utilities.RestAPIHelper<CreateEditProvinceViewModel>
                                            .Submit(json, Method.PUT, endpoint2, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }

            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditCity(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditCityViewModel model = new CreateEditCityViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetCity/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditCityViewModel>
                                            .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditCity", model);
        }

        [HttpPost]
        public ActionResult EditCity(CreateEditCityViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var endpoint = url + "/ValidateCityCode/" + model.Code + "/" + model.ID;
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, endpoint);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("City Code has been used"));
                }
                else
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();

                    string json = JsonConvert.SerializeObject(model);
                    var endpoint2 = url + "/EditCity";
                    model = Utilities.RestAPIHelper<CreateEditCityViewModel>
                                            .Submit(json, Method.PUT, endpoint2, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }

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

        [HttpPost]
        public ActionResult DeleteProvince(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + "/DeleteProvince/" + json;
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

        [HttpPost]
        public ActionResult DeleteCity(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + "/DeleteCity/" + json;
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

        [HttpGet]
        public ActionResult ValidateCode(string code, string id)
        {
            var endpoint = url + "/ValidateCode/" + code + "/" + id;
            var isLevelCodeValid = Utilities.RestAPIHelper<List<bool>>
                                        .Submit("", Method.GET, endpoint);
            return Json(isLevelCodeValid);
        }
    }
}