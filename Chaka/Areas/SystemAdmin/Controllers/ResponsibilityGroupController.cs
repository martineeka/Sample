using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.ResponsibilityGroup;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class ResponsibilityGroupController : Controller
    {
        string url = ApiUrl.SecurityUrl + "ResponsibilityGroup";
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

                var urlEndPoint = url + Route.Get;

                var query = Utilities.RestAPIHelper<CustomDataSourceResult<ListResponsibilityGroupViewModel>>
                                     .Submit(json, Method.POST, urlEndPoint, Request);

                newDataSourceResult.Data = query.data;
                newDataSourceResult.Total = query.total;

                foreach (var item in query.data)
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
            //var decryptedID = Convert.ToInt32(model.ID);
            //model.ID = decryptedID.ToString();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}{1}", url, Route.Validate);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}{1}", url, Route.Add);
                    model = Utilities.RestAPIHelper<CreateEditViewModel>
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
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            CreateEditViewModel model = new CreateEditViewModel();
            try
            {
                string json = "";
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Get + "/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditViewModel>
                                    .Submit(json, Method.GET, endpoint, Request);
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
            //var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            //model.ID = decryptedID.ToString();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}{1}", url, Route.Validate);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = url + Route.Edit;
                    Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint, Request);
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
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Delete, json);
                var result = Utilities.RestAPIHelper<string>.Submit("", Method.POST, endpoint, Request);

                if (Convert.ToBoolean(result))
                    ajaxViewModel.SetValues(false, null, "Responsibility Group already used by User");
                else
                    ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public IActionResult IndexRespGroupDetail(string resGroupID)
        {
            var model = new IndexRespGroupDetailViewModel();

            int decryptedHeaderID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(resGroupID));
            try
            {
                var endpoint = url + "/GetResponsibilityGroup/" + decryptedHeaderID;

                model = Utilities.RestAPIHelper<IndexRespGroupDetailViewModel>
                                          .Submit("", Method.GET, endpoint, Request);

                model.ID = decryptedHeaderID.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return View(model);
        }

        public IActionResult ListRespGroupDetail([DataSourceRequest] DataSourceRequest request, string HeaderID)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {

                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                int ID = Convert.ToInt32(HeaderID);
                var urlEndPoint = url + "/GetListRespGroupDetail/" + ID;

                var query = Utilities.RestAPIHelper<CustomDataSourceResult<ListRespGroupDetailViewModel>>
                                     .Submit(json, Method.POST, urlEndPoint, Request);

                newDataSourceResult.Data = query.data;
                newDataSourceResult.Total = query.total;

                foreach (var item in query.data)
                {
                    item.ID = EncryptionHelper.EncryptUrlParam(item.ID);
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(newDataSourceResult);
        }

        public ActionResult CreateRespGroupDetail(string headerID)
        {
            var model = new CreateEditRespGroupDetailViewModel();
            model.ResponsibilityGroupID = Convert.ToInt32(headerID);
            return PartialView("CreateEditRespGroupDetail", model);
        }

        [HttpPost]
        public ActionResult CreateRespGroupDetail(CreateEditRespGroupDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/ValidateIsAxists", url);
                var isAxists = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isAxists.IsSuccess)
                {
                    jsonViewModel.SetValues(isAxists.IsSuccess, null, isAxists.Message);
                }
                else
                {
                    endpoint = url + "/AddRespGroupDetail";
                    var content = Utilities.RestAPIHelper<CreateEditRespGroupDetailViewModel>
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

        public ActionResult GetResponsibility()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetResponsibility";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public ActionResult EditRespGroupDetail(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditRespGroupDetailViewModel model = new CreateEditRespGroupDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetRespGroupDetail/" + decryptID;

                model = Utilities.RestAPIHelper<CreateEditRespGroupDetailViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditRespGroupDetail", model);
        }

        [HttpPost]
        public ActionResult EditRespGroupDetail(CreateEditRespGroupDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/ValidateIsAxists", url);
                var isAxists = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isAxists.IsSuccess)
                {
                    jsonViewModel.SetValues(isAxists.IsSuccess, null, isAxists.Message);
                }
                else
                {
                    endpoint = url + "/EditRespGroupDetail";

                    model = Utilities.RestAPIHelper<CreateEditRespGroupDetailViewModel>
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
        public ActionResult DeleteRespGroupDetail(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {

                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = String.Format("{0}/DeleteRespGroupDetail/{1}", url, json);

                Utilities.RestAPIHelper<string>
                                      .Submit("", Method.POST, endpoint, Request);

                ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }
    }
}