using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterLocationViewModel
    {
        public int EmployeeListFilterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] LocationNotSelected { get; set; }
        public string[] LocationSelected { get; set; }

        public IEnumerable<ListLocationViewModel> ListLocation { get; set; }
        public IEnumerable<ListLocationViewModel> ListLocationSelected { get; set; }
    }
}
