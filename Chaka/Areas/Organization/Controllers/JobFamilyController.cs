using System;
using System.Linq;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobFamily;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Kendo.Mvc.Extensions;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class JobFamilyController : Controller
    {
        string url = String.Format("{0}{1}", ApiUrl.OrganizationUrl, "JobFamily");
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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobFamilyViewModel>>
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
        
        public JsonResult MajorList([DataSourceRequest] DataSourceRequest request, string jobFamilyID)
        {
            jobFamilyID = jobFamilyID ?? (string)TempData.Peek("ID");
            TempData["ID"] = jobFamilyID;

            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetMajorList/" + jobFamilyID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDetailViewModel>>
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

        public ActionResult Edit(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditViewModel model = new CreateEditViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Get, decryptID); 
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
                                  .Submit("", Method.GET, url + "/ValidateJobFamilyCode/" + model.Code + "/" + model.ID);
                if (Convert.ToBoolean(checkCode) == true)
                {
                    jsonViewModel.SetValues(false, null, String.Format("Job Family Code has been used"));
                }
                else
                {
                    var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
                    model.ID = decryptedID.ToString();

                    string json = JsonConvert.SerializeObject(model);
                    var endpoint = url + Route.Edit;
                    model = Utilities.RestAPIHelper<CreateEditViewModel>
                                            .Submit(json, Method.PUT, endpoint);
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
                var delete = Utilities.RestAPIHelper<AjaxViewModel>
                                      .Submit("", Method.POST, endpoint);
                ajaxViewModel.SetValues(delete.IsSuccess, null, delete.Message);
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }


       

        public ActionResult IndexDetail(string headerID)
        {
            var viewModel = new IndexDetailViewModel();
            try
            {
                var endpoint = String.Format("{0}{1}/{2}", url, Route.GetDetail, headerID);
                viewModel = Utilities.RestAPIHelper<IndexDetailViewModel>
                                          .Submit("", Method.GET, endpoint);
                viewModel.EncryptJobFamilyID = headerID;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }


        public ActionResult EditDetail(string headerID)
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
            return PartialView("CreateEditDetail", model);
        }

        public JsonResult ListUnselected([DataSourceRequest] DataSourceRequest request, int jobFamilyID, string[] selectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListUnselected", jobFamilyID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDetailViewModel>>.Submit(jsonBody, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }


        public JsonResult ListSelected([DataSourceRequest] DataSourceRequest request, int jobFamilyID, string[] unselectedID)
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

                var endpoint = String.Format("{0}/{1}/{2}/{3}", url, "ListSelected", jobFamilyID, json);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListDetailViewModel>>.Submit(jsonBody, Method.POST, endpoint);
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
                var result = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit(json, Method.POST, endpoint);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var ajaxViewModel = new AjaxViewModel(false, null, String.Format("Failed\nMessage : {0}", ex.GetBaseException().Message));
                return Json(ajaxViewModel);
            }
        }

      
    }
}