using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using Kendo.Mvc.Extensions;
using Chaka.ViewModels.Organization.OrganizationReport;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class OrganizationReportController : ControllerBase
    {
        private readonly IOrganizationReportService _organizationReportProvider;
        private IMapper _mapper;

        public OrganizationReportController(IOrganizationReportService organizationReportProvider, IMapper mapper)
        {
            _organizationReportProvider = organizationReportProvider;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetReport")]
        public IActionResult GetReport()
        {

            var orgReport = _organizationReportProvider.GetReport();
            if (orgReport == null)
            {
                return NotFound("Organization Report Not Found.");
            }
            return Ok(orgReport);
        }

        [HttpGet]
        [Route("GetReportTreeView")]
        public IActionResult GetReportTreeView()
        {

            var orgReport = _organizationReportProvider.GetReportTreeView();
            if (orgReport == null)
            {
                return NotFound("Organization Report Not Found.");
            }
            return Ok(orgReport);
        }

        [HttpPost]
        [Route("Get/{startDate}/{endDate}")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request, string startDate, string endDate)
        {
            try
            {
                DateTime StartDate  = DateTime.ParseExact(startDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                DateTime EndDate = DateTime.ParseExact(endDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);

                var OrgLogChange = _organizationReportProvider.GetList(StartDate, EndDate);
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = OrgLogChange.ToDataSourceResult(req);

                return Ok(filter);
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}