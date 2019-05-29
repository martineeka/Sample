using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class IndexFilterLevelViewModel
    {
        public int EmployeeListFilterID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] LevelNotSelected { get; set; }
        public string[] LevelSelected { get; set; }

        public IEnumerable<ListLevelViewModel> ListLevel { get; set; }
        public IEnumerable<ListLevelViewModel> ListLevelSelected { get; set; }
    }
}
