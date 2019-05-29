using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Chaka.ViewModels.CustomModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Employee.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);


            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(authHeader.Parameter);

            var jsonToken = JsonConvert.SerializeObject(securityToken);

            var previewObject = JsonConvert.DeserializeObject<CustomTokenModel>(jsonToken);

            return new JsonResult(securityToken);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
