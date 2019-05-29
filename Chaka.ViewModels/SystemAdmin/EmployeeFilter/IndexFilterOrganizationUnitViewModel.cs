using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterOrganizationUnitViewModel
    {
        public int EmployeeListFilterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] OrganizationUnitNotSelected { get; set; }
        public string[] OrganizationUnitSelected { get; set; }

        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnit { get; set; }
        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitSelected { get; set; }
    }
}
