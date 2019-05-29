using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Chaka.Providers;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.CustomModel;
using Chaka.ViewModels.Organization.OrganizationReport;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class OrganizationReportController : Controller
    {
        string url = String.Format("{0}OrganizationReport", ApiUrl.OrganizationUrl);

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.EffectiveDate = DateTime.Now;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult ParseParam(IndexViewModel model)
        {
            model.OrganizationUnitID = EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID);
            return Json(model);
        }
        public ActionResult OrgUnitHierarchy(int organizationUnitId, DateTime effectiveDate)
        {
            var endpoint = String.Format("{0}/{1}", url, "GetReport");
            var content = Utilities.RestAPIHelper<List<ReportOrganizationChart>>.Submit("", Method.GET, endpoint);
            var filter = Filter(content, organizationUnitId, effectiveDate) as List<ReportOrganizationChart>;
            var result = ToHierarchy(filter);

            ViewData["reOrgChart"] = result;
            return View();
        }
        public ActionResult OrgUnitTreeView(int organizationUnitId, string typeChart, DateTime effectiveDate)
        {
            var endpoint = String.Format("{0}/{1}", url, "GetReportTreeView");
            var content = Utilities.RestAPIHelper<List<ReportOrganizationChartTreeView>>.Submit("", Method.GET, endpoint);
            var filter = Filter(content, organizationUnitId, effectiveDate) as List<ReportOrganizationChartTreeView>;
            var result = ToTreeView(filter);

            var json = JsonConvert.SerializeObject(result);


            ViewData["reOrgChartTreeView"] = json;
            return View();
        }
        private IEnumerable<ReportOrganizationChartTreeView> Filter(List<ReportOrganizationChartTreeView> list, int organizationUnitId, DateTime effectiveDate)
        {
            var filter = new List<ReportOrganizationChartTreeView>();
            foreach (var item in list)
            {
                if (item.id == organizationUnitId && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                {
                    filter.Add(item);
                }
                if (item.ParentID == organizationUnitId && item.ParentID > 0 && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                {
                    foreach (var items in list)
                    {
                        if (items.ParentID == item.id && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                        {
                            filter.Add(items);
                        }
                    }
                    filter.Add(item);
                }
                filter = filter.OrderBy(o => o.id).ToList();
            }
            return filter;
        }
        public IEnumerable<ReportOrganizationChartTreeView> ToTreeView(List<ReportOrganizationChartTreeView> list)
        {
            var lookup = new Dictionary<int, ReportOrganizationChartTreeView>();
            var nested = new List<ReportOrganizationChartTreeView>();

            foreach (ReportOrganizationChartTreeView item in list)
            {
                item.ParentID = item.ParentID == null ? 0 : item.ParentID;
                if (lookup.ContainsKey((int)item.ParentID))
                {
                    lookup[(int)item.ParentID].items.Add(item);
                }
                else
                {
                    nested.Add(item);
                }
                lookup.Add(item.id, item);
            }
            return nested;
        }


        private IEnumerable<ReportOrganizationChart> Filter(List<ReportOrganizationChart> list, int organizationUnitId, DateTime effectiveDate)
        {
            var filter = new List<ReportOrganizationChart>();
            foreach (var item in list)
            {
                if (item.ID == organizationUnitId && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                {
                    filter.Add(item);
                }
                if (item.ParentID == organizationUnitId && item.ParentID > 0 && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                {
                    foreach (var items in list)
                    {
                        if (items.ParentID == item.ID && (effectiveDate >= item.BeginEff && (item.LastEff >= effectiveDate || item.LastEff == null)))
                        {
                            filter.Add(items);
                        }
                    }
                    filter.Add(item);
                }
                filter = filter.OrderBy(o => o.ID).ToList();
            }
            return filter;
        }
        public IEnumerable<ReportOrganizationChart> ToHierarchy(List<ReportOrganizationChart> list)
        {
            var lookup = new Dictionary<int, ReportOrganizationChart>();
            var nested = new List<ReportOrganizationChart>();

            foreach (ReportOrganizationChart item in list)
            {
                item.ParentID = item.ParentID == null ? 0 : item.ParentID;
                if (lookup.ContainsKey((int)item.ParentID))
                {
                    lookup[(int)item.ParentID].items.Add(item);
                }
                else
                {
                    nested.Add(item);
                }
                lookup.Add(item.ID, item);
            }
            return nested;
        }

        #region Log Change
        public IActionResult IndexLog()
        {
            var model = new IndexLogViewModel();
            model.StartDate = DateTime.Today;
            return View(model);
        }
        public ActionResult ParseParamLog(IndexLogViewModel model)
        {
            return Json(model);
        }
        public ActionResult OrgLogChange(DateTime startDate, DateTime endDate)
        {
            var model = new IndexLogViewModel();
            model.StartDate = startDate;
            model.EndDate = endDate;
            return View(model);
        }
        public JsonResult List([DataSourceRequest] DataSourceRequest request, DateTime startDate, DateTime endDate)
        {
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {

                var startDateFormat = startDate.ToString("yyyyMMddHHmmss");
                var endDateFormat = endDate.ToString("yyyyMMddHHmmss");
                var jsonBody = Utilities.GridUtilities.ConvertKendoRequestToJson(request);

                var endpoint = String.Format("{0}{1}/{2}/{3}", url, Route.Get, startDateFormat, endDateFormat);

                var result = Utilities.RestAPIHelper<CustomDataSourceResult<ListReportLogChange>>
                                      .Submit(jsonBody, Method.POST, endpoint);

                newDataSourceResult.Data = result.data;
                newDataSourceResult.Total = result.total;

                foreach (var item in result.data)
                {
                    if (item.ChangeDate == null && item.DeletedDate == null)
                    {
                        item.Status = StatusLogChange.NEW;
                    }
                    else if (item.ChangeDate.HasValue && item.DeletedDate == null)
                    {
                        item.Status = StatusLogChange.CHANGE;
                    }
                    else if (item.DeletedDate.HasValue)
                    {
                        item.Status = StatusLogChange.DELETE;
                        item.DeletedBy = item.ChangeBy;
                    }
                }
            }
            catch (Exception ex)
            {
                jsonViewModel.SetValues(false, null, String.Format("Failed\\nMessage: {0}", ex.GetBaseException().Message));
            }
            return Json(newDataSourceResult);
        }

        #endregion Log Change

        //[HttpPost]
        //public ActionResult Excel_Export_Save(string contentType, string base64, string fileName)
        //{
        //    var fileContents = Convert.FromBase64String(base64);

        //    return File(fileContents, contentType, fileName);
        //}

        //[HttpPost]
        //public ActionResult Pdf_Export_Save(string contentType, string base64, string fileName)
        //{
        //    var fileContents = Convert.FromBase64String(base64);

        //    return File(fileContents, contentType, fileName);
        //}
    }
}