using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnitChange
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [Display(Name = "Organization Unit")]
        public string OrganizationUnitID { get; set; }
        public string OrganizationUnit { get; set; }

        [Required]
        [Display(Name = "Begin Effective")]
        public DateTime? BeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? LastEff { get; set; }

        [Required]
        [Display(Name = "Reason")]
        [StringLength(500, ErrorMessage = "Maximum 500 character")]
        public string Reason { get; set; }
    }
}
