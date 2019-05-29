using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SysAdmin
{
    public class ListUserViewModel
    {
        public string ID { get; set; }
        public string EmployeeID { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string UserStatusName { get; set; }
        public string Person { get; set; }
        public string Company { get; set; }
        public string CurrentBussinessGroupID { get; set; }
        public string Filter { get; set; }
        public DateTime? passwordExpiration { get; set; }

    }
}
