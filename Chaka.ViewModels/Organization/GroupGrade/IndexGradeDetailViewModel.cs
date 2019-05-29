using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.GroupGrade
{
    public class IndexGradeDetailViewModel
    {
        public string GroupGradeID { get; set; }
        [Display(Name = "Group Code")]
        public string GroupCode { get; set; }
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }

        public string Description { get; set; }
        public IEnumerable<ListGradeDetailViewModel> ListGrade { get; set; }
    }
}
