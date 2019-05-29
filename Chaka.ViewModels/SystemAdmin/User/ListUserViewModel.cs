using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.User
{
    public class ListUserViewModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nik { get; set; }
        public string Employee { get; set; }
        public string Filter { get; set; }
        public string Status { get; set; }
    }
}
