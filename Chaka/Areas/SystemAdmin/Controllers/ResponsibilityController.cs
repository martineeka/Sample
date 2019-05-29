using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.SystemAdmin.Responsibility;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
//using System.Web.Mvc;

namespace Chaka.Areas.SystemAdmin.Controllers
{
    [Area("SystemAdmin")]
    public class ResponsibilityController : Controller
    {
        string url = ApiUrl.SecurityUrl + "Responsibility";
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
                DataClaim.GetClaim(Request);
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);

                var urlEndPoint = url + Route.Get;

                var query = Utilities.RestAPIHelper<CustomDataSourceResult<ListResponsibiltyViewModel>>
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
            var decryptedID = Convert.ToInt32(model.ID);
            model.ID = decryptedID.ToString();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + Route.Add;
                model = Utilities.RestAPIHelper<CreateEditViewModel>
                                          .Submit(json, Method.POST, endpoint, Request);

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
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.ID));
            model.ID = decryptedID.ToString();
            var jsonViewModel = new AjaxViewModel();
            try
            {
                string json = JsonConvert.SerializeObject(model);
                var endpoint = url + Route.Edit;
                Utilities.RestAPIHelper<string>
                                 .Submit(json, Method.PUT, endpoint, Request);
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
                var endpoint = String.Format("{0}{1}/{2}", url, Route.Delete, json);
                var result = Utilities.RestAPIHelper<string>.Submit("", Method.POST, endpoint, Request);
                if (Convert.ToBoolean(result))
                    ajaxViewModel.SetValues(false, null, "Responsibility already used by Responsibility Group Detail");
                else
                    ajaxViewModel.SetValues(true, null, "Deleted Successfully");
            }
            catch (Exception ex)
            {
                ajaxViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }

            return Json(ajaxViewModel);
        }

        public ActionResult GetMenus()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + Route.Get + "Menu";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                        .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }

        public ActionResult GetBusinessGroup()
        {
            List<DropDownListViewModel> model = new List<DropDownListViewModel>();
            try
            {
                var endpoint = url + Route.Get + "BussinesGroup";
                model = Utilities.RestAPIHelper<List<DropDownListViewModel>>
                                                 .Submit("", Method.GET, endpoint, Request);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(model);
        }
    }
}