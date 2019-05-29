using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterJobTitleViewModel
    {
        public int EmployeeListFilterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] JobTitleNotSelected { get; set; }
        public string[] JobTitleSelected { get; set; }

        public IEnumerable<ListJobTitleViewModel> ListJobTitle { get; set; }
        public IEnumerable<ListJobTitleViewModel> ListJobTitleSelected { get; set; }
    }
}
