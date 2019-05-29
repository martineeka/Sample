using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Chaka.ViewModels.SystemAdmin.EmployeeFilter;
using Newtonsoft.Json;
 

namespace Chaka.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class EmployeeFilterController : Controller
    {

        string url = ApiUrl.SecurityUrl + "EmployeeFilter";
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult List([DataSourceRequest] DataSourceRequest request)
        {
            var token = Request.Cookies["token"];

            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = url + Route.Get;

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListEmployeeListFilter>>
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
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + Route.Add;
                var content = Utilities.RestAPIHelper<CreateEditViewModel>
                                          .Submit(json, Method.POST, endpoint);
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


        public ActionResult ValidateFilterCode(string code, string id)
        {
            bool isFilterCodeValid;
            try
            {
                int decryptedID = id == "" ? 0 : Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
                var endpoint = url + Route.Validate + "/" + code + "/" + decryptedID;
                isFilterCodeValid = Utilities.RestAPIHelper<bool>
                                       .Submit("", Method.POST, endpoint);
            }
            catch (Exception)
            {
                throw;
            }
            return Json(isFilterCodeValid);
        }
        

        public ActionResult IndexDetail(string ID)
        {
            var jsonViewModel = new AjaxViewModel();
            ListEmployeeListFilter model = new ListEmployeeListFilter();
            try
            {
                var endpoint = url + "/GetDetail/" + ID; 
                model = Utilities.RestAPIHelper<ListEmployeeListFilter>
                                            .Submit("", Method.GET, endpoint);
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            //return PartialView("CreateMenuDetail", model);
            return PartialView(model);
            
        }

        #region level
        public JsonResult ListSelectedLevel([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListLevelSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListLevelViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedLevel([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListLevelUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListLevelViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        #endregion

        #region grade
        public JsonResult ListSelectedGade([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListGradeSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListGradeViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedGrade([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListGradeUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListGradeViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }
        #endregion

        #region Job title
        public JsonResult ListSelectedJobTitle([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListJobTitleSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobTitleViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedJobTitle([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListJobTitleUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListJobTitleViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        #endregion


        #region Location
        public JsonResult ListSelectedLocation([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListLocationSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListLocationViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedLocation([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListLocationUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListLocationViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        #endregion

        #region OrganizationUnit
        public JsonResult ListSelectedOrganizationUnit([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListOrganizationUnitSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListOrganizationUnitViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedOrganizationUnit([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListOrganizationUnitUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListOrganizationUnitViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        #endregion

        #region EmployeeStatus
        public JsonResult ListSelectedEmployeeStatus([DataSourceRequest] DataSourceRequest request, int ID, string[] unselectedID)
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
                string jsonTemp = jsonBody.Substring(0, jsonBody.Length - 5);
                jsonTemp = jsonTemp + json + "}";
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListEmployeeStatusSelected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListEmployeeStatusViewModel>>.Submit(jsonTemp, Method.POST, endpoint);
                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

            }
            catch (Exception)
            {

                throw;
            }
            return Json(newDataSourceResult);

        }

        public JsonResult ListUnselectedEmployeeStatus([DataSourceRequest] DataSourceRequest request, int ID, string[] selectedID)
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
                var endpoint = String.Format("{0}/{1}/{2}", url, "ListEmployeeStatusUnselected", ID);
                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListEmployeeStatusViewModel>>.Submit(jsonTemp, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;
            }
            catch (Exception)
            {
                throw;
            }
            return Json(newDataSourceResult);
        }

        #endregion





        //public IActionResult IndexDetail(string ID)
        //{
        //    var model = new ListEmployeeListFilter();
        //    int decryptID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(ID));

        //    var endpoint = url + "/Get/" + decryptID;
        //    var filter = Utilities.RestAPIHelper<ListEmployeeListFilter>.Submit("", Method.GET, endpoint);

        //    model.ID = ID;
        //    model.Code = filter.Code;
        //    model.Name = filter.Name;
        //    model.Description = filter.Description;

        //    return View(model);

        //}

    }
}