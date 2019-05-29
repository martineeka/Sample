using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.OrgUnitChange;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class OrganizationUnitChangeController : Controller
    {
        string url = ApiUrl.OrganizationUrl + "OrganizationUnitChange";

        #region Organization Unit Change
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

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListOrgUnitChangeViewModel>>
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
            model.BeginEff = DateTime.Today;
            return PartialView("CreateEdit", model);
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
                model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, endpoint);
                //model.OrganizationUnitID = EncryptionHelper.EncryptUrlParam(Convert.ToString(model.OrganizationUnitID));
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

                var endpoint = url + Route.Edit;
                model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint);
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
                var delete = Utilities.RestAPIHelper<AjaxViewModel>.Submit("", Method.DELETE, endpoint);
                ajaxViewModel.SetValues(delete.IsSuccess, null, delete.Message);
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }
        #endregion Organization Unit Change

        #region Organization Unit Change Detail
        public IActionResult IndexDetail(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            var model = new IndexDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetDetailIndex/" + decryptID;
                model = Utilities.RestAPIHelper<IndexDetailViewModel>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return View(model);
        }

        public JsonResult DetailList([DataSourceRequest] DataSourceRequest request, string orgChangeID)
        {
            orgChangeID = orgChangeID ?? (string)TempData.Peek("ID");
            TempData["ID"] = orgChangeID;

            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + "/GetDetailList/" + orgChangeID;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListOrgUnitChangeDetailViewModel>>
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

        public ActionResult CreateDetail(int orgChangeID)
        {
            var model = new CreateEditDetailViewModel();
            model.OrgUnitTransactionID = orgChangeID;
            model.BeginEff = DateTime.Today;
            model.StatusID = 1;
            return PartialView("CreateEditDetail", model);
        }

        [HttpPost]
        public ActionResult CreateDetail(CreateEditDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);

                var endpoint = url + "/AddDetail";
                var content = Utilities.RestAPIHelper<CreateEditDetailViewModel>.Submit(json, Method.POST, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditDetail(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditDetailViewModel model = new CreateEditDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetDetail/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditDetailViewModel>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditDetail", model);
        }

        [HttpPost]
        public ActionResult EditDetail(CreateEditDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);

                var endpoint = url + "/EditDetail";
                model = Utilities.RestAPIHelper<CreateEditDetailViewModel>.Submit(json, Method.PUT, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult NonActiveDetail(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditDetailViewModel model = new CreateEditDetailViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + "/GetDetail/" + decryptID;
                model = Utilities.RestAPIHelper<CreateEditDetailViewModel>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditDetail", model);
        }

        [HttpPost]
        public ActionResult NonActiveDetail(CreateEditDetailViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);

                var endpoint = url + "/DeadActiveDetail";
                model = Utilities.RestAPIHelper<CreateEditDetailViewModel>.Submit(json, Method.PUT, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public JsonResult GetCostCenter()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/CostCenter";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetOrgLevel()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/OrgLevel";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetLegalEntityInformation()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/LegalEntityInformation";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetCategory()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + "/Category";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }
        #endregion Organization Unit Change Detail

    }
}