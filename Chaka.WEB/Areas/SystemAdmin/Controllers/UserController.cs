using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Kendo.Mvc.Extensions;
using Chaka.ViewModels.SysAdmin;
using Newtonsoft.Json;

namespace Chaka.WEB.Areas.SystemAdmin.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List([DataSourceRequest] DataSourceRequest request)
        {
            var client = new RestClient("https://localhost:44378/api/User/List");
            var requests = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(requests);
            var content = response.Content;

            return Json(content.ToDataSourceResult(request));
        }

        public ActionResult Create()
        {
            var model = new CreateEditViewModel();
            //model.EmployeeListFilterLookUp = userProvider.GetEmployeeListFilterLookUp();
            //model.ResponsibilityGroupLookUp = userProvider.GetResponsibilityGroupLookUp();
            return PartialView("CreateEdit", model);
        }

        public ActionResult Create(CreateEditViewModel model)
        {
            try
            {
                string modelJSON = JsonConvert.SerializeObject(model);

                var client = new RestClient("https://localhost:44378/api/User/Create");
                var request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddParameter("application/json", modelJSON, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                return Ok(content);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

    }
}
