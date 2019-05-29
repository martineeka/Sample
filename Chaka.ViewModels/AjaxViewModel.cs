using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels
{
    public class AjaxViewModel
    {
        public AjaxViewModel()
        {
        }

        public AjaxViewModel(bool isSuccess, object data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public void SetValues(bool isSuccess, object data, string message)
        {
            IsSuccess = isSuccess;
            Data = data;
            Message = message;
        }

        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }

    public class AjaxLoginViewModel 
    {
        public AjaxLoginViewModel()
        {
        }

        public string BearerToken { get; set; }

        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }


        public AjaxLoginViewModel(bool isSuccess, string bearerToken, Data data, string message)
        {
            IsSuccess = isSuccess;
            BearerToken = bearerToken;
            Data = data;
            Message = message;
        }

        public void SetValuesLogin(bool isSuccess, string bearerToken,  Data data, string message)
        {
            IsSuccess = isSuccess;
            BearerToken = bearerToken;
            Data = data;
            Message = message;
        }
    }
    public class Data
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string loginName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public object passwordExpiration { get; set; }
        public int errorCounter { get; set; }
        public int employeeId { get; set; }
        public int employeeListFilterId { get; set; }
        public int userStatusId { get; set; }
        public DateTime lastLogin { get; set; }
        public int currentBusinessGroupId { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public object delDate { get; set; }
        public object userStatus { get; set; }
        public object usersImei { get; set; }
    }
}
