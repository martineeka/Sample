using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.OrgUnit;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Newtonsoft.Json;
using Chaka.Database.Context.Models;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]

    public class OrganizationUnitController : Controller
    {
        string url = String.Format("{0}OrganizationUnit", ApiUrl.OrganizationUrl);

        


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
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListOrgUnitViewModel>>
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

        [HttpGet]
        public ActionResult CreateEditOrganizationUnit(string idHeader)
        {
            var ID = (idHeader != null) ? Utilities.EncryptionHelper.EncryptUrlParam(idHeader) : "0";
            var model = new CreateEditViewModel();
            if (ID == "0")
            {
                var today = DateTime.Today;
                ID = "0";
                model.headerID = ID;
                model.BeginEff = new DateTime(today.Year, today.Month, today.Day);
            }
            else
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(idHeader));
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Get, decryptID);
                model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, endpoint);

              
                model.headerID = ID;
                TempData["ID"] = model.headerID;
                ViewBag.Action = "Edit Employee Relation";
            }
            return PartialView("CreateEditOrganizationUnit", model);

        }

        public ActionResult Create(string idHeader)
        {
            var ID = (idHeader != null) ? Utilities.EncryptionHelper.EncryptUrlParam(idHeader) : "0";
            var model = new CreateEditViewModel();
            model.BeginEff = DateTime.Today;
            model.headerID = ID;
            TempData["ID"] = model.headerID;
            return PartialView("CreateEdit", model);
        }

        [HttpPost]
        public ActionResult Create(CreateEditViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/ValidateCombination", url);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    if (model.IsLegalEntity)
                    {
                        endpoint = String.Format("{0}/AddWithLegalEntity", url);
                        var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.POST, endpoint);
                        jsonViewModel.SetValues(true, null, "Saved");
                    }
                    else {
                        endpoint = url + Route.Add;
                        var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.POST, endpoint);

                        jsonViewModel.SetValues(true, null, "Saved");
                       


                    }
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
                var orgUnit = Utilities.RestAPIHelper<OrgUnit>.Submit("", Method.GET, endpoint);
                model = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, endpoint);
                TempData["ID"] = orgUnit.Id;
                model.ID = id;
                model.TFNRegisterDate = DateTime.Today;
                model.SPPKPReleaseDate = DateTime.Today;
                model.TDPBeginEff = DateTime.Today;
                model.TDPLastEff = DateTime.Today;
                if (model.SuperiorID != null)
                {
                    model.SuperiorID = EncryptionHelper.EncryptUrlParam(model.SuperiorID);
                }
                var endpoints = String.Format("{0}/GetSelectedEmployee/{1}", url, orgUnit.SuperiorId);
                var selectedEmployee = Utilities.RestAPIHelper<Employee>.Submit("", Method.GET, endpoints);

                model.SuperiorCode = orgUnit.SuperiorId != null
                    ? selectedEmployee.Nik : "";

                var endpointss = String.Format("{0}/GetCurrentEmployeeFullName/{1}", url, orgUnit.SuperiorId);
                var fullName = Utilities.RestAPIHelper<string>.Submit("", Method.GET, endpointss);
                model.SuperiorName = orgUnit.SuperiorId != null
               ? fullName : "";
                if (model.ParentID != null)
                {
                    model.ParentID = EncryptionHelper.EncryptUrlParam(model.ParentID);
                }
                var endpointParent = String.Format("{0}{1}/{2}", url, Route.Get, (int)orgUnit.ParentId);
                var parent = Utilities.RestAPIHelper<OrgUnit>.Submit("", Method.GET, endpointParent);
                model.ParentName = orgUnit.ParentId != null ? parent.Name : "";
                if (orgUnit.LegalEntityInformationId != null)
                {
                    var legalEntityCreateEdit = String.Format("{0}/GetLegalEntityCreateEditViewModel{1}", url, model);
                    var legalEntityCreateEditViewModel = Utilities.RestAPIHelper<CreateEditViewModel>.Submit("", Method.GET, legalEntityCreateEdit);
                    model = legalEntityCreateEditViewModel;
                    endpoints = String.Format("{0}/GetSelectedEmployee/{1}", url, orgUnit.LegalEntityInformation.ManagingDirectorId);
                    selectedEmployee = Utilities.RestAPIHelper<Employee>.Submit("", Method.GET, endpoints);
                    model.ManagingDirectorCode = selectedEmployee.Nik;
                    endpointss = String.Format("{0}/GetCurrentEmployeeFullName/{1}", url, orgUnit.SuperiorId);
                    fullName = Utilities.RestAPIHelper<string>.Submit("", Method.GET, endpointss);
                    model.ManagingDirectorName = fullName;
                }
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
                var endpoint = String.Format("{0}/ValidateCombination", url);
                var isValid = Utilities.RestAPIHelper<AjaxViewModel>.Submit(json, Method.POST, endpoint);
                if (!isValid.IsSuccess)
                {
                    jsonViewModel.SetValues(isValid.IsSuccess, null, isValid.Message);
                }
                else
                {
                    if (model.IsLegalEntity)
                    {
                        endpoint = String.Format("{0}/EditWithLegalEntity", url);
                        var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint);
                        jsonViewModel.SetValues(true, null, "Saved");
                    }
                    else
                    {
                        if (model.LegalEntityInformationID.HasValue)
                        {
                              endpoint = String.Format("{0}/DeleteLegalEntity/{1}", url, model.LegalEntityInformationID.GetValueOrDefault());

                              Utilities.RestAPIHelper<string>.Submit("", Method.DELETE, endpoint);
                        }
                        endpoint = url + Route.Edit;
                        var content = Utilities.RestAPIHelper<CreateEditViewModel>.Submit(json, Method.PUT, endpoint);
                        jsonViewModel.SetValues(true, null, "Saved");
                    }
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        [HttpGet]
        public ActionResult LegalEntity(string orgUnitID)
        {
           var endpoints = String.Format("{0}/GetLegalEntity/{1}", url, orgUnitID);
           var model = Utilities.RestAPIHelper<LegalEntityViewModel>.Submit("", Method.GET, endpoints);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Delete, json);

                Utilities.RestAPIHelper<string>.Submit("", Method.DELETE, endpoint);
                ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public JsonResult GetCostCenter()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/CostCenter", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetOrganizationUnitLevel()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/OrganizationUnitLevel", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult  GetOrganizationUnitCategory()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/OrganizationUnitCategory", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetParentalOrgUnit(string id, int categoryTypeID, string text)
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/ParentalOrgUnit /{1}/{2}/{3}", url,id,categoryTypeID,text);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        #region LegalEntity
        public JsonResult GetCurrency()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetCurrency", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetKPPBranch()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetKPPBranch", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetBusinessFieldRegulation()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetBusinessFieldRegulation", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetBusinessFieldCategories( int businessFieldRegulationID, string text)
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetBusinessFieldCategories/{1}/{2}", url, businessFieldRegulationID, text);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetBusinessFieldClassification(int businessFieldCategoryID, string text)
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetBusinessFieldClassification/{1}/{2}", url, businessFieldCategoryID, text);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public JsonResult GetSIUPClasses()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/GetSIUPClasses", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        #endregion

        #region List Job Title

        public IActionResult IndexJobTitle(string organizationID)
        {
            var viewModel = new IndexJobTitleViewModel();
            var decryptedHeaderID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(organizationID));
            try
            {
                var endpoint = String.Format("{0}/GetIndexJobTitle/{1}", url, decryptedHeaderID);

                viewModel = Utilities.RestAPIHelper<IndexJobTitleViewModel>
                                          .Submit("", Method.GET, endpoint);
                viewModel.OrganizationID = organizationID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        public JsonResult ListJobTitle([DataSourceRequest] DataSourceRequest request, string organizationID )
        {

            // Must Convert to Json from kendo data source request , because filter using abstract class
            organizationID = organizationID ?? EncryptionHelper.EncryptUrlParam(TempData.Peek("ID").ToString());
            int decryptedId = 0 ;
            if (organizationID != "0") { decryptedId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(organizationID)); }
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);

                var endpoint = String.Format("{0}/GetListJobTitle/{1}", url, decryptedId);

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobTitleViewModel>>
                                      .Submit(json, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
                TempData["ID"] = organizationID;
                foreach (var item in result.data)
                {
                    item.ID = EncryptionHelper.EncryptUrlParam(item.ID);
                    item.OrganizationUnitID= item.OrganizationUnitID;
                    item.ValidFrom = item.ValidFrom;
                    item.ValidTo =item.ValidTo;
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        public ActionResult CreateJobTitle(string organizationID)
        {

            var orgID = 0;
            var model = new CreateEditJobTitleViewModel();
            if (organizationID != null) { orgID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(organizationID)); }
            model.OrganizationID = Convert.ToString(orgID);
            model.ValidFrom = DateTime.Now;

            return PartialView("CreateEditJobTitle", model);
        }

        [HttpPost]
        public ActionResult CreateJobTitle(CreateEditJobTitleViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/AddJobTitle/{1}", url,model);
                var content = Utilities.RestAPIHelper<string>
                        .Submit(json, Method.POST, endpoint);
                model.ID = content;
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditJobTitle(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditJobTitleViewModel model = new CreateEditJobTitleViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}/GetJobTitle/{1}", url, decryptID);

                model = Utilities.RestAPIHelper<CreateEditJobTitleViewModel>
                                            .Submit("", Method.GET, endpoint);
                string encryptID = EncryptionHelper.EncryptUrlParam(model.OrganizationID);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditJobTitle", model);
        }

        [HttpPost]
        public ActionResult EditJobTitle(CreateEditJobTitleViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/EditJobTitle", url);

                model = Utilities.RestAPIHelper<CreateEditJobTitleViewModel>
                                        .Submit(json, Method.PUT, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult DeleteJobTitle(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = String.Format("{0}/DeleteJobTitle/{1}", url, json);

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

        public JsonResult GetDropDownJobTitle()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/DropDownJobTitle", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        #endregion

        #region List Job Location

        public IActionResult IndexJobLocation(string organizationID)
        {
            var viewModel = new IndexLocationViewModel();
            var decryptedHeaderID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(organizationID));
            try
            {
                var endpoint = String.Format("{0}/GetIndexLocation/{1}", url, decryptedHeaderID);

                viewModel = Utilities.RestAPIHelper<IndexLocationViewModel>
                                          .Submit("", Method.GET, endpoint);
                viewModel.OrganizationID = organizationID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(viewModel);
        }

        public JsonResult ListLocation([DataSourceRequest] DataSourceRequest request, string organizationID = null)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            organizationID = organizationID ?? EncryptionHelper.EncryptUrlParam(TempData.Peek("ID").ToString());
            var decryptedId = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(organizationID));
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);

                var endpoint = String.Format("{0}/GetListLocation/{1}", url, decryptedId);

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListLocationDetailViewModel>>
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
            }
            return Json(newDataSourceResult);
        }

        public ActionResult CreateLocation(string organizationID)
        {
            var model = new CreateEditLocationViewModel();
            model.OrganizationID = organizationID;

            return PartialView("CreateEditLocation", model);
        }

        [HttpPost]
        public ActionResult CreateLocation(CreateEditLocationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);

                var endpoint = String.Format("{0}/AddLocation/", url);
                var content = Utilities.RestAPIHelper<CreateEditLocationViewModel>
                    .Submit(json, Method.POST, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");

            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        public ActionResult EditLocation(string id)
        {
            var jsonViewModel = new AjaxViewModel();
            CreateEditLocationViewModel model = new CreateEditLocationViewModel();
            try
            {
                int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = String.Format("{0}/GetLocation/{1}", url, decryptID);

                model = Utilities.RestAPIHelper<CreateEditLocationViewModel>
                                            .Submit("", Method.GET, endpoint);
                string encryptID = EncryptionHelper.EncryptUrlParam(model.OrganizationID);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return PartialView("CreateEditLocation", model);
        }

        [HttpPost]
        public ActionResult EditLocation(CreateEditLocationViewModel model)
        {
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = String.Format("{0}/EditLocation", url);

                model = Utilities.RestAPIHelper<CreateEditLocationViewModel>
                                        .Submit(json, Method.PUT, endpoint);
                jsonViewModel.SetValues(true, null, "Saved");
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(jsonViewModel);
        }

        [HttpPost]
        public ActionResult DeleteLocation(string[] arrayOfID)
        {
            var ajaxViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(arrayOfID);
                var endpoint = String.Format("{0}/DeleteLocation/{1}", url, json);

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

        public JsonResult GetDropDownLocation()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = String.Format("{0}/DropDownLocation", url);

                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>.Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }
        #endregion


    }
}