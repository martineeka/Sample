using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobTitle;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class JobTitleController : Controller
    {
        string url = ApiUrl.OrganizationUrl + "JobTitle";

        #region Job Title
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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobTitleViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

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
            model.BeginEff = DateTime.Today;
            //return View("CreateEdit", model);
            return PartialView("CreateEdit", model);
        }

        public ActionResult CreateNewPage()
        {
            var model = new IndexNewPageViewModel();
            model.BeginEff = DateTime.Today;
            return View("Create", model);
            //return PartialView("CreateEdit", model);
        }

        public ActionResult GetDataDescription(string headerID)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new IndexNewPageViewModel();
            try
            {
                var endpoint = String.Format("{0}/{1}/{2}", url, "GetDataDescription", headerID);
                model = Utilities.RestAPIHelper<IndexNewPageViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("DataDescription", model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateCombination";
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = url + Route.Add;
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
                var endpoint = url + Route.Get + "/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, endpoint, Request);
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
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + "/ValidateCombination";
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint, Request);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = url + Route.Edit;
                    model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint, Request);
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
                var delete = Utilities.RestAPIHelper<AjaxViewModel>.Submit("", Method.DELETE, endpoint, Request);

                ajaxViewModel.SetValues(delete.IsSuccess, null, delete.Message);
                //ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public JsonResult GetJobStatus()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/JobStatus";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetLevel()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/Level";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetLevelCategory(int levelID)
        {
            DropDownListViewModel model = new DropDownListViewModel();
            try
            {
                var endpoint = url + "/LevelCategory/" + levelID;
                model = Utilities.RestAPIHelper<DropDownListViewModel>.Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetJobFamily()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetJobFamily";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public ActionResult ValidateJobTitleCode(string code, string id)
        {
            var endpoint = url + "/ValidateJobTitleCode/" + code + "/" + id;
            var isJobTitleCodeValid = Utilities.RestAPIHelper<List<bool>>.Submit("", Method.GET, endpoint, Request);
            return Json(isJobTitleCodeValid);
        }
        #endregion Job Title

        #region Job Description
        public IActionResult IndexDescription(string JobTitleID)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new IndexDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(JobTitleID));
                var endpoint = url + "/GetDetailDesc/" + decryptID;
                model = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit("", Method.GET, endpoint, Request);

            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return View(model);
        }

        public JsonResult DescriptionList([DataSourceRequest] DataSourceRequest request, string jobTitleID)
        {
            jobTitleID = jobTitleID ?? (string)TempData.Peek("ID");
            TempData["ID"] = jobTitleID;

            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetDescriptionList/" + jobTitleID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobTitleViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        public ActionResult EditDescription(string headerID)
        {
            var jsonViewModel = new AjaxViewModel();
            IndexDetailViewModel model = new IndexDetailViewModel();
            try
            {
                var endpoint = String.Format("{0}/{1}/{2}", url, "GetDetailDesc", headerID);
                model = Utilities.RestAPIHelper<IndexDetailViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditDescription", model);
        }

        public JsonResult ListDescUnselected([DataSourceRequest] DataSourceRequest request, int jobTitleID, string[] selectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListUnselected", jobTitleID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDescriptionViewModel>>.Submit(jsonBody, Method.POST, endpoint, Request);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }


        public JsonResult ListDescSelected([DataSourceRequest] DataSourceRequest request, int jobTitleID, string[] unselectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListSelected", jobTitleID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDescriptionViewModel>>.Submit(jsonBody, Method.POST, endpoint, Request);
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
        public ActionResult UpdateDescription(IndexDetailViewModel model)
        {
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/{1}", url, "UpdateDescription");
                var result = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit(json, Method.POST, endpoint, Request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
                return Json(ajaxViewModel);
            }
        }

        #endregion Job Description

        #region Job Spesification
        public IActionResult IndexSfesification(string JobTitleID)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new IndexDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(JobTitleID));
                var endpoint = url + "/GetDetail/" + decryptID;
                model = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit("", Method.GET, endpoint, Request);
                model.EncryptedJobTitleID = JobTitleID;
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return View(model);
        }

        public JsonResult SpesificationList([DataSourceRequest] DataSourceRequest request, string jobTitleID)
        {
            jobTitleID = jobTitleID ?? (string)TempData.Peek("ID");
            TempData["ID"] = jobTitleID;

            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetSpesificationList/" + jobTitleID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListSpesificationViewModel>>
                                      .Submit(json, Method.POST, endpoint, Request);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        public ActionResult CreateSpecification(string headerID)
        {
            var model = new CreateEditSpecificationViewModel();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "GetDetailSpecification", headerID, 0);
                model = Utilities.RestAPIHelper<CreateEditSpecificationViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return PartialView("CreateEditSpecification", model);
        }

        [HttpPost]
        public ActionResult CreateSpecification(CreateEditSpecificationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/{1}", url, "SpecificationValidateCombination");
                var check = Utilities.RestAPIHelper<string>
                                            .Submit(json, Method.POST, endpoint, Request);
                if(!Convert.ToBoolean(check))
                {
                    endpoint = String.Format("{0}/{1}", url, "AddSpecification");
                    var content = Utilities.RestAPIHelper<CreateEditSpecificationViewModel>
                                                .Submit(json, Method.POST, endpoint, Request);
                    return RedirectToAction("Index");
                }
                else
                {
                    jsonViewModel.SetValues(false, null, String.Format("Specification has been exist"));
                    return Json(jsonViewModel);
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
                return Json(jsonViewModel);
            }
        }

        public ActionResult EditSpecification(string JobTitleID, string id)
        {
            var model = new CreateEditSpecificationViewModel();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "GetDetailSpecification", JobTitleID, id);
                model = Utilities.RestAPIHelper<CreateEditSpecificationViewModel>
                                            .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return PartialView("CreateEditSpecification", model);
        }

        [HttpPost]
        public ActionResult EditSpecification(CreateEditSpecificationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                model.ID = EncryptionHelper.DecryptUrlParam(model.ID);
                model.JobTitleID = EncryptionHelper.DecryptUrlParam(model.JobTitleID);
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/{1}", url, "SpecificationValidateCombination");
                var check = Utilities.RestAPIHelper<string>
                                            .Submit(json, Method.POST, endpoint, Request);
                if(!Convert.ToBoolean(check))
                {
                    endpoint = String.Format("{0}/{1}", url, "EditSpecification");
                    var content = Utilities.RestAPIHelper<CreateEditSpecificationViewModel>
                                                .Submit(json, Method.POST, endpoint, Request);
                    return RedirectToAction("Index");
                }
                else
                {
                    jsonViewModel.SetValues(false, null, String.Format("Specification has been exist"));
                    return Json(jsonViewModel);
                }
                
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
                return Json(jsonViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteSpecification(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = url + "/DeleteSpecification/" + json;
                Utilities.RestAPIHelper<string>.Submit("", Method.DELETE, endpoint, Request);
                ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public JsonResult ListMajorUnselected([DataSourceRequest] DataSourceRequest request, int jobTitleID, string[] selectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListMajorUnselected", jobTitleID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDescriptionViewModel>>.Submit(jsonBody, Method.POST, endpoint, Request);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        public JsonResult ListMajorSelected([DataSourceRequest] DataSourceRequest request, int jobTitleID, string[] unselectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListMajorSelected", jobTitleID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDescriptionViewModel>>.Submit(jsonBody, Method.POST, endpoint, Request);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult GetLineOfBusiness()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetLineOfBusiness";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetJobFunction()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetJobFunction";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetLevelEdu()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/GetLevelEdu";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }
        #endregion Job Spesification
    }
}