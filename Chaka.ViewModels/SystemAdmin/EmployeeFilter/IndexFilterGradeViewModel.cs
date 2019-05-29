using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterGradeViewModel
    {
        public int EmployeeListFilterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] GradeNotSelected { get; set; }
        public string[] GradeSelected { get; set; }

        public IEnumerable<ListGradeViewModel> ListGrade { get; set; }
        public IEnumerable<ListGradeViewModel> ListGradeSelected { get; set; }
    }
}
