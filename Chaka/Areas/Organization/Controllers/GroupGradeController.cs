using System;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.GroupGrade;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class GroupGradeController : Controller
    {

        string url = String.Format("{0}GroupGrade", ApiUrl.OrganizationUrl);
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
                var endpoint = String.Format("{0}{1}", url, Route.Get);
                var result = RestAPIHelper<CustomDataSourceResult<ListGroupGradeViewModel>>.Submit(json, Method.POST, endpoint, Request);
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
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}{1}", url, Route.Add);
                    var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.POST, endpoint, Request);
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
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Get, decryptID);
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
                //var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                //model.ID = decryptedID.ToString();
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}{1}", url, Route.Validate);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}{1}", url, Route.Edit);

                    model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint, Request);
                    jsonViewModel.SetValues(true, null, "Saved");
                }
                //model = Utilities.RestAPIHelper<CreateEditViewModel>
                //                        .Submit(json, Method.PUT, endpoint);
                //jsonViewModel.SetValues(true, null, "Saved");
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
                    ajaxViewModel.SetValues(false, null, "Grade already used by Employee");
                else
                    ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public ActionResult ValidateGroupGradeCode(string code, string id)
        {
            bool isGroupGradeCodeValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}{1}/{2}/{3}", url, Route.Validate, code, decryptedID);
                isGroupGradeCodeValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint, Request);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isGroupGradeCodeValid);
        }

        public ActionResult ValidateGradeCode(string code, string id)
        {
            bool isGradeCodeValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}{1}/{2}/{3}", url, Route.Validate, code, decryptedID);

                isGradeCodeValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint, Request);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isGradeCodeValid);
        }

        public ActionResult ValidateGradeDetailName(string name, string id)
        {
            bool isGradeCodeValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}{1}/{2}/{3}", url, Route.Validate, name, decryptedID);

                isGradeCodeValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint, Request);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isGradeCodeValid);
        }

        public IActionResult IndexGradeDetail(string groupGradeID)
        {
            var viewModel = new IndexGradeDetailViewModel();
            var decryptedHeaderID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(groupGradeID));
            try
            {
                var endpoint = String.Format("{0}/GetIndexGradeDetail/{1}", url, decryptedHeaderID);

                viewModel = Utilities.RestAPIHelper<IndexGradeDetailViewModel>
                                          .Submit("", Method.GET, endpoint, Request);
                viewModel.GroupGradeID = groupGradeID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        public JsonResult ListGradeDetail([DataSourceRequest] DataSourceRequest request, string headerID = null)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            headerID = headerID ?? EncryptionHelper.EncryptUrlParam(TempData.Peek("ID").ToString());
            var decryptedId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(headerID));
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);

                var endpoint = String.Format("{0}/GetListGradeDetail/{1}", url, decryptedId);

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListGradeDetailViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

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
            }
            return Json(newDataSourceResult);
        }

        public ActionResult CreateGradeDetail(string headerID)
        {
            var model = new CreateEditGradeDetailViewModel();
            model.GroupGradeID = headerID;

            var endpoint = String.Format("{0}/MaxSequenceLevel/{1}", url, headerID);
            model.MaxSequence = Utilities.RestAPIHelper<int>
                                          .Submit("", Method.GET, endpoint, Request);
            return PartialView("CreateEditGradeDetail", model);
        }

        [HttpPost]
        public ActionResult CreateGradeDetail(CreateEditGradeDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/ValidateGrade", url);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}/AddGradeDetail/", url);
                    var content = Utilities.RestAPIHelper<CreateEditGradeDetailViewModel>
                        .Submit(json, Method.POST, endpoint);
                    jsonViewModel.SetValues(true, null, "Saved");
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditGradeDetail(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditGradeDetailViewModel model = new CreateEditGradeDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}/GetGradeDetail/{1}", url, decryptID);

                model = Utilities.RestAPIHelper<CreateEditGradeDetailViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
                string encryptID = EncryptionHelper.EncryptUrlParam(model.GroupGradeID);

                var maxSequence = String.Format("{0}/MaxSequenceLevel/{1}", url, encryptID);

                model.MaxSequence = Utilities.RestAPIHelper<int>
                                              .Submit("", Method.GET, maxSequence, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditGradeDetail", model);
        }

        [HttpPost]
        public ActionResult EditGradeDetail(CreateEditGradeDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/ValidateGrade", url);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = String.Format("{0}/EditGradeDetail", url);

                    model = Utilities.RestAPIHelper<CreateEditGradeDetailViewModel>
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
        public ActionResult DeleteGradeDetail(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {

                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = String.Format("{0}/DeleteGradeDetail/{1}", url, json);

                var result = Utilities.RestAPIHelper<string>
                                      .Submit("", Method.POST, endpoint, Request);

                if (Convert.ToBoolean(result))
                    ajaxViewModel.SetValues(false, null, "Grade already used by Employee");
                else
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