using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterAllViewModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] LevelNotSelected { get; set; }
        public string[] LevelSelected { get; set; }
        public string[] GradeNotSelected { get; set; }
        public string[] GradeSelected { get; set; }
        public string[] JobTitleNotSelected { get; set; }
        public string[] JobTitleSelected { get; set; }
        public string[] LocationNotSelected { get; set; }
        public string[] LocationSelected { get; set; }
        public string[] OrganizationUnitNotSelected { get; set; }
        public string[] OrganizationUnitSelected { get; set; }
        public string[] EmployeeStatusNotSelected { get; set; }
        public string[] EmployeeStatusSelected { get; set; }

        public IEnumerable<ListLevelViewModel> ListLevel { get; set; }
        public IEnumerable<ListLevelViewModel> ListLevelSelected { get; set; }

        public IEnumerable<ListGradeViewModel> ListGrade { get; set; }
        public IEnumerable<ListGradeViewModel> ListGradeSelected { get; set; }

        public IEnumerable<ListJobTitleViewModel> ListJobTitle { get; set; }
        public IEnumerable<ListJobTitleViewModel> ListJobTitleSelected { get; set; }

        public IEnumerable<ListLocationViewModel> ListLocation { get; set; }
        public IEnumerable<ListLocationViewModel> ListLocationSelected { get; set; }

        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnit { get; set; }
        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitSelected { get; set; }

        public IEnumerable<ListEmployeeStatusViewModel> ListEmployeeStatus { get; set; }
        public IEnumerable<ListEmployeeStatusViewModel> ListEmployeeStatusSelected { get; set; }






    }
}
