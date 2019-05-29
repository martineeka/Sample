using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chaka.Providers.Organization;
using Chaka.Utilities;
using Chaka.ViewModels.CustomModel;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.Api.Organization.Controllers
{
    [Route("api/Organization/[controller]")]
    [ApiController]
    public class BrowseOrganizationUnitController : ControllerBase
    {
        private readonly IBrowseOrganizationUnitProviderService _browseProvider;
        private IMapper _mapper;

        public BrowseOrganizationUnitController(IBrowseOrganizationUnitProviderService browseProvider, IMapper mapper)
        {
            _browseProvider = browseProvider;
            _mapper = mapper;
        }

        #region Browse
        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var jobTitle = _browseProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = jobTitle.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        #endregion Browse

    }
}