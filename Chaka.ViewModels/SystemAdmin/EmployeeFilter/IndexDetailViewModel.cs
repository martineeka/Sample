using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexDetailViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] EmployeeNotSelected { get; set; }
        public string[] EmployeeSelected { get; set; }

        public IEnumerable<ListEmployeeStatusViewModel> ListEmployee { get; set; }
        public IEnumerable<ListEmployeeStatusViewModel> ListEmployeeSelected { get; set; }
    }
}
