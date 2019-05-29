using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.BusinessFieldRegulation;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class BusinessFieldRegulationController : Controller
    {
        string url = String.Format("{0}{1}", ApiUrl.OrganizationUrl, "BusinessFieldRegulation");

        #region BusinessFieldREgulation
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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListBusinessFieldRegulationViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

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
                model.ID = "0";
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, url + "/ValidateCodeExist/" + model.Code, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
                }
                else
                {
                    string json = JsonConvert.SerializeObject(model);
                    var endpoint = url + Route.Add;
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

        public ActionResult Edit(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditViewModel model = new CreateEditViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Get + "/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
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
                                  .Submit("", Method.GET, url + "/ValidateCode/" + model.Code + "/" + model.ID, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
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

        [HttpPost]
        public ActionResult Delete(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + Route.Delete + "/" + json;
                var result = Utilities.RestAPIHelper<AjaxViewModel>
                                      .Submit("", Method.POST, endpoint, Request);
                ajaxViewModel.SetValues(result.IsSuccess, null, result.Message);
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }
        #endregion BusinessFieldRegulation

        #region BusinessFieldCategory
        public IActionResult IndexCategory(string BusinessFieldRegulationID)
        {
            var model = new ViewModels.Organization.BusinessFieldRegulation.IndexBusinessFieldRegulationViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(BusinessFieldRegulationID));
            var endpoint = url + "/GetCategory/" + decryptID;
            var data = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, endpoint, Request);
            model.ID = data.ID;
            model.Code = data.Code;
            model.Name = data.Name;
            model.Description = data.Description;
            model.IsActive = data.IsActive;
            return View(model);
        }

        public JsonResult ListCategory([DataSourceRequest] DataSourceRequest request, string BusinessFieldRegulationID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetCategory/" + BusinessFieldRegulationID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListBusinessFieldCategoryViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

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

        public ActionResult CreateCategory(string headerID)
        {
            var model = new CreateEditCategoryViewModel();
            model.BusinessFieldRegulationID = headerID;
            return PartialView("CreateEditCategory", model);
        }

        [HttpPost]
        public ActionResult CreateCategory(CreateEditCategoryViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();

            try
            {
                model.ID = "0";
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, url + "/ValidateCodeExistCategory/" + model.Code, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
                }
                else
                {
                    string json = JsonConvert.SerializeObject(model);
                    var endpoint = url + "/AddCategory";
                    var content = Utilities.RestAPIHelper<CreateEditCategoryViewModel>
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

        public ActionResult EditCategory(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new CreateEditCategoryViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetCategory/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditCategoryViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditCategory", model);
        }

        [HttpPost]
        public ActionResult EditCategory(CreateEditCategoryViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit("", Method.GET, url + "/ValidateCodeCategory/" + model.Code + "/" + model.ID, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
                }
                else
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();

                    string json = JsonConvert.SerializeObject(model);
                    var endpoint = url + "/EditCategory";
                    model = Utilities.RestAPIHelper<CreateEditCategoryViewModel>
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

        [HttpPost]
        public ActionResult DeleteCategory(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + "/DeleteCategory/" + json;
                var result = Utilities.RestAPIHelper<AjaxViewModel>
                                      .Submit("", Method.POST, endpoint, Request);
                ajaxViewModel.SetValues(result.IsSuccess, null, result.Message);
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }
        #endregion BusinessFieldCategory

        #region FieldClassification
        public IActionResult IndexClassification(string BusinessFieldCategoryID, string BusinessFieldRegulationID)
        {
            var model = new ViewModels.Organization.BusinessFieldRegulation.IndexBusinessFieldCategoryViewModel();
            int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(BusinessFieldCategoryID));
            var endpoint = url +"/GetWithRegulationName" + "/" + decryptID;
            var data = Utilities.RestAPIHelper<IndexBusinessFieldCategoryViewModel>.Submit("", Method.GET, endpoint, Request);
            model.ID = data.ID;
            model.Code = data.Code;
            model.Name = data.Name;
            model.RegulationName = data.RegulationName;
            model.BusinessFieldRegulationID = BusinessFieldRegulationID;
            
            return View(model);
        }

        public JsonResult ListClassification([DataSourceRequest] DataSourceRequest request, string BusinessFieldCategoryID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetClassification/" + BusinessFieldCategoryID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListBusinessFieldClassificationViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

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

        public ActionResult CreateClassification(string headerID)
        {
            var model = new CreateEditClassificationViewModel();
            model.BusinessFieldCategoryID = headerID;
            return PartialView("CreateEditClassification", model);
        }

        [HttpPost]
        public ActionResult CreateClassification(CreateEditClassificationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();

            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateCodeExistClassification";
                var checkCode = Utilities.RestAPIHelper<string>
                                    .Submit(json, Method.POST, endpoint, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
                }
                else
                {

                    endpoint = url + "/AddClassification";
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

        public ActionResult EditClassification(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new CreateEditClassificationViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetClassification/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditClassificationViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditClassification", model);
        }

        [HttpPost]
        public ActionResult EditClassification(CreateEditClassificationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                model.ID = decryptedID.ToString();

                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateCodeClassification";
                var checkCode = Utilities.RestAPIHelper<string>
                                  .Submit(json, Method.POST, endpoint, Request);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Code has been used"));
                }
                else
                {
                    endpoint = url + "/EditClassification";
                    model = Utilities.RestAPIHelper<CreateEditClassificationViewModel>
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

        [HttpPost]
        public ActionResult DeleteClassification(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + "/DeleteClassification/" + json;
                var result = Utilities.RestAPIHelper<AjaxViewModel>
                                      .Submit("", Method.POST, endpoint, Request);
                ajaxViewModel.SetValues(result.IsSuccess, null, result.Message);
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }
        #endregion FIeldClassification
    }
}