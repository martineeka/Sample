using Chaka.ViewModels.CustomModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Chaka.Utilities
{

    public static class Context
    {
        private static IHttpContextAccessor HttpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        private static Uri GetAbsoluteUri()
        {
            var request = HttpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();
            return uriBuilder.Uri;
        }
    }

    public class ApiUrl
    {
        public static string OrganizationUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlOrganization}/Organization/";
        public static string EmployeeUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlEmployee}/Employee/";
        public static string TimeUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlTime}/Time/";
        public static string RecruitmentUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlRecruitment}/Recruitment/";
        public static string CareerUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlCareer}/Career/";
        public static string LearningUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlLearning}/Learning/";
        public static string PayrollUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlPayroll}/Payroll/";
        public static string PerformanceUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlPerformance}/Performance/";
        public static string ReimbursementUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlReimbursement}/Reimbursement/";
        public static string CompetencyUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlCompetency}/Competency/";
        public static string LoanAdministrationUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlLoanAdministration}/LoanAdministration/";
        public static string SecurityUrl => $"{DataConfiguration.Configuration.AppConfiguration.ApiServerUrlSecurity}/Security/";

    }

    public class Route
    {
        public static readonly string Get = "/Get";
        public static readonly string GetDetail = "/GetDetail";
        public static readonly string Add = "/Add";
        public static readonly string Edit = "/Edit";
        public static readonly string Delete = "/Delete";
        public static readonly string Validate = "/Validate";
        public static readonly string MaxSequenceLevel = "/MaxSequenceLevel";
    }
    public class DataConfiguration
    {
        public static Configuration Configuration
        {
            get
            {
                string json = System.IO.File.ReadAllText("appsettings.json");
                var data = JsonConvert.DeserializeObject<Configuration>(json);
                return data;
            }
        }
    }
    public class Configuration
    {
        public AppConfiguration AppConfiguration { get; set; }
    }
    public class AppConfiguration
    {
        public string ApiServerUrlSecurity { get; set; }
        public string ApiServerUrlOrganization { get; set; }
        public string ApiServerUrlEmployee { get; set; }
        public string ApiServerUrlTime { get; set; }
        public string ApiServerUrlRecruitment { get; set; }
        public string ApiServerUrlCareer { get; set; }
        public string ApiServerUrlLearning { get; set; }
        public string ApiServerUrlPayroll { get; set; }
        public string ApiServerUrlPerformance { get; set; }
        public string ApiServerUrlReimbursement { get; set; }
        public string ApiServerUrlCompetency { get; set; }
        public string ApiServerUrlLoanAdministration { get; set; }
    }
    public class RestAPIHelper<T>
    {
        public static T Submit(string jsonBody, Method httpMethod, string endpoint)
        {
            var requests = new RestRequest(httpMethod);
            requests.AddHeader("Content-Type", "application/json");
            requests.AddParameter("Authorization", string.Format("Bearer " + ""), ParameterType.HttpHeader);


            if (!string.IsNullOrEmpty(jsonBody))
            {
                requests.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            }

            var client = new RestClient(endpoint);
            IRestResponse response = client.Execute(requests);
            var result = JsonConvert.DeserializeObject<T>(response.Content);

            return result;
        }

        public static T Submit(string jsonBody, Method httpMethod, string endpoint, HttpRequest httpRequest)
        {

            var requests = new RestRequest(httpMethod);
            requests.AddHeader("Content-Type", "application/json");
            requests.AddHeader("Authorization", string.Format("Bearer " + httpRequest.Cookies["token"]));

            if (!string.IsNullOrEmpty(jsonBody))
            {
                requests.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
            }
            var client = new RestClient(endpoint);
            IRestResponse response = client.Execute(requests);

            var result = JsonConvert.DeserializeObject<T>(response.Content);

            return result;
        }

    }
    public class TokenDataClaim
    {
        public static int CurrentUserID { get; set; }
        public static int CurrentBusinessGrupID { get; set; }
        public static string CurrentEmail { get; set; }
        public static string LoginName { get; set; }
        public static string Password { get; set; }
    }


    public class DataClaim
    {
        public static string Token { get; set; }
        public static TokenDataClaim GetClaim(HttpRequest Request)
        {
            var handler = new JwtSecurityTokenHandler();
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var securityToken = handler.ReadToken(authHeader.Parameter);
            var jsonToken = JsonConvert.SerializeObject(securityToken);
            var previewObject = JsonConvert.DeserializeObject<CustomTokenModel>(jsonToken);
            TokenDataClaim claim = new TokenDataClaim();
            foreach (var a in previewObject.claims)
            {
                switch (a.type.ToString().ToLower())
                {
                    case "currentuserid":
                        TokenDataClaim.CurrentUserID = Int32.Parse(a.value);
                        break;
                    case "currentbusinessgrupid":
                        TokenDataClaim.CurrentBusinessGrupID = Int32.Parse(a.value);
                        break;
                    case "currentemail":
                        TokenDataClaim.CurrentEmail = a.value;
                        break;
                    case "nbf":
                        //claim.LoginName = a.value;
                        break;
                    case "exp":
                        //claim.Password = a.value;
                        break;
                    case "iat":
                        TokenDataClaim.CurrentUserID = Int32.Parse(a.value);
                        break;
                    case "password":
                        TokenDataClaim.Password = a.value;
                        break;
                    case "loginname":
                        TokenDataClaim.LoginName = a.value;
                        break;
                }
            }
            return claim;
        }


    }

}
