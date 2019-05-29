using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Browse;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace Chaka.Areas.Organization.Controllers
{
    [Area("Organization")]
    public class BrowseSuperiorController : Controller
    {
        string url = String.Format("{0}BrowseSuperior", ApiUrl.OrganizationUrl);

        public IActionResult Index(string callBack)
        {
            var model = new IndexBrowseSuperiorViewModel();
            model.Callback = callBack;
            return PartialView("Index", model);
        }

        [HttpPost]
        public ActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            // Must Convert to Json from kendo data source request , because filter using abstract class
            AjaxViewModel jsonViewModel = new AjaxViewModel();
            DataSourceResult newDataSourceResult = new DataSourceResult();
            try
            {
                var json = Utilities.GridUtilities.ConvertKendoRequestToJson(request);
                var endpoint = String.Format("{0}{1}", url, Route.Get);


                var result = Utilities.RestAPIHelper<CustomDataSourceResult<BrowseSuperiorViewModel>>
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

    }
}