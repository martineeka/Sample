using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrganizationReport
{
    public class IndexViewModel
    {
        [Required]
        [Display(Name = "Organization Unit")]
        public string OrganizationUnitID { get; set; }
        [Required]
        [Display(Name = "Organization Unit")]
        public string OrganizationUnit { get; set; }

        [Display(Name = "Type Chart")]
        public string TypeChart { get; set; }

        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }
    }
}
