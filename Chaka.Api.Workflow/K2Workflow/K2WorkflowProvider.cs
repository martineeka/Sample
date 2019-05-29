using Chaka.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static Chaka.Api.Workflow.K2Workflow.K2WorklistModel;

namespace Chaka.Api.Workflow.K2Workflow
{
    public class K2WorkflowProvider
    {
        public static HttpClient k2CloudClient;

        public K2WorkflowProvider(string userName, string password)
        {

            // Providing static credentials for this demo, using a client handler to store credentials. 
            // Consider using integrated security to maintain stronger security in applications.
            //NetworkCredential k2credentials = new NetworkCredential(userName, RijndaelHelper.Decrypt(password, "IGLO2015"));
            NetworkCredential k2credentials = new NetworkCredential(userName, password);
            HttpClientHandler loginHandler = new HttpClientHandler
            {
                Credentials = k2credentials
            };

            k2CloudClient = new HttpClient(loginHandler, true);
        }

        public async Task<HttpResponseMessage> StartK2WorkflowAsync(int workflowid, string instancedata)
        {
            //TODO: make this endpoint a configuration setting, or pass in the URI for the method. 
            // TODO: Replace [K2RESTSERVICEBASEURL] token accordingly.
            // K2 REST endpoint for starting a workflow. 
            //string requesturi = "https://k2.denallix.com/Api/Workflow/V1/workflows/" + workflowid;
            string requesturi = "http://dev-chaka/Api/Workflow/V1/workflows/" + workflowid;

            HttpResponseMessage response;

            // Create a string content class to encode JSON data before it gets sent to K2 in the REST call. The async POST method wants an object that
            // implements the HttpContent class for this.
            StringContent datacontent = new StringContent(instancedata, Encoding.UTF8, "application/json");

            //Start the workflow with an asynchronous POST call so you don't block your application 
            response = await k2CloudClient.PostAsync(requesturi, datacontent);

            return response;
        }

        public async Task<HttpResponseMessage> CustomActionK2Task(string serialnumber, string customaction, string userdefinedfields)
        {

            // TODO: Make this endpoint uri a configurable setting or pass it into the Method.
            // TODO: Replace [K2RESTSERVICEBASEURL] token accordingly.
            // Custom task action endpoint format. This should be set as a configuration setting. Showing this here for demo purposes. 
            string requestUri = "http://dev-chaka/api/workflow/v1/tasks/" + serialnumber + "/actions/" + customaction;
            StringContent dataContent = new StringContent(userdefinedfields, Encoding.UTF8, "application/json");

            return await k2CloudClient.PostAsync(requestUri, dataContent);

        }

        public async Task<K2TaskList> GetK2TaskListAsync(string state)
        {
            // Using a class with DataContract, which will allow easier serialization/deserialization of JSON to K2 Tasklist object.
            // K2TaskList objects were generated from JSON output coming from K2 Rest "https://{K2 Server-Domain}/api/workflow/v1/Tasks?State={All|Allocated|Sleeping}" method call.
            K2TaskList k2tasklist = new K2TaskList();

            // TODO: Make this endpoint a configurable setting. 
            // TODO: Replace [K2RESTSERVICEBASEURL] token accordingly.
            // Get task list REST endpoint from K2 server. 
            string requesturi = "http://dev-chaka/api/workflow/v1/tasks/";

            string responsebody = await k2CloudClient.GetStringAsync(requesturi);

            // Deserialize the response into the K2TaskList object as defined by data contract for this object.
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(responsebody)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(k2tasklist.GetType());
                k2tasklist = ser.ReadObject(ms) as K2TaskList;
            }

            return k2tasklist;
        }
    }
}
