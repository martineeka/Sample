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
    public class BrowseSuperiorController : ControllerBase
    {
        private readonly IBrowseSuperiorProviderService _browseProvider;
        private IMapper _mapper;

        public BrowseSuperiorController(IBrowseSuperiorProviderService browseProvider, IMapper mapper)
        {
            _browseProvider = browseProvider;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpPost]
        [Route("Get")]
        public IActionResult Get([FromBody]CustomDataSourceRequest request)
        {
            try
            {
                var superior = _browseProvider.GetList();
                DataSourceRequest req = GridUtilities.ConvertToKendoFromCustomRequest(request);
                var filter = superior.ToDataSourceResult(req);
                return Ok(filter);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}