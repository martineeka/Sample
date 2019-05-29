using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Chaka.Api.Workflow.K2Workflow;
using Chaka.ViewModels.CustomModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chaka.Api.Workflow.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {


        [HttpPost]
        [Route("GetTask")]
        public async Task<IActionResult> GetTask()
        {
            try
            {
                //var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);


                //var handler = new JwtSecurityTokenHandler();
                //var securityToken = handler.ReadToken(authHeader.Parameter);

                //var jsonToken = JsonConvert.SerializeObject(securityToken);

                //var previewObject = JsonConvert.DeserializeObject<CustomTokenModel>(jsonToken);

                //K2WorkflowProvider workflow = new K2WorkflowProvider(
                //    previewObject.claims.SingleOrDefault(x => x.type == "LoginName").value,
                //    previewObject.claims.SingleOrDefault(x => x.type == "Password").value);
                K2WorkflowProvider workflow = new K2WorkflowProvider("k2services", "Medion.2019");

                var tasks = await workflow.GetK2TaskListAsync("All");

                return Ok(tasks);
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [Route("Start")]
        public async Task<IActionResult> Start([FromBody] K2StartProcessModel model)
        {
            try
            {
                //var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);


                //var handler = new JwtSecurityTokenHandler();
                //var securityToken = handler.ReadToken(authHeader.Parameter);

                //var jsonToken = JsonConvert.SerializeObject(securityToken);

                //var previewObject = JsonConvert.DeserializeObject<CustomTokenModel>(jsonToken);

                //K2WorkflowProvider workflow = new K2WorkflowProvider(
                //    previewObject.claims.SingleOrDefault(x => x.type == "LoginName").value,
                //    previewObject.claims.SingleOrDefault(x => x.type == "Password").value);
                K2WorkflowProvider workflow = new K2WorkflowProvider("k2services", "Medion.2019");

                var tasks = await workflow.StartK2WorkflowAsync(model.WorkflowId, JsonConvert.SerializeObject(model.Instance));

                return Ok(tasks);
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        [Route("Action")]
        public async Task<IActionResult> Action(string serialNumber)
        {
            try
            {
                //var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);


                //var handler = new JwtSecurityTokenHandler();
                //var securityToken = handler.ReadToken(authHeader.Parameter);

                //var jsonToken = JsonConvert.SerializeObject(securityToken);

                //var previewObject = JsonConvert.DeserializeObject<CustomTokenModel>(jsonToken);

                //K2WorkflowProvider workflow = new K2WorkflowProvider(
                //    previewObject.claims.SingleOrDefault(x => x.type == "LoginName").value,
                //    previewObject.claims.SingleOrDefault(x => x.type == "Password").value);
                K2WorkflowProvider workflow = new K2WorkflowProvider("k2services", "Medion.2019");

                var tasks = await workflow.CustomActionK2Task(serialNumber, "approve", "");

                return Ok(tasks);
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}