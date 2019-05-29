using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.JobMaster;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class JobMasterController : Controller
    {
        string url = String.Format("{0}JobMaster", ApiUrl.OrganizationUrl);


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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobMasterViewModel>>
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
                    var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.POST, endpoint);
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
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}{1}", url, Route.Validate);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    endpoint = url + Route.Edit;
                    model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint);
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

        public ActionResult ValidateJobMasterName(string name, string id)
        {
            bool isJobMasterNameValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Validate + "/" + name + "/" + decryptedID;
                isJobMasterNameValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isJobMasterNameValid);
        }

        public ActionResult ValidateJobMasterCode(string code, string id)
        {
            bool isJobmasterCodeValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Validate + "/" + code + "/" + decryptedID;
                isJobmasterCodeValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isJobmasterCodeValid);
        }

    }
}