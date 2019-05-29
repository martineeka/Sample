using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnitChange
{
    public class CreateEditDetailViewModel
    {
        public string ID { get; set; }

        public int OrgUnitTransactionID { get; set; }
        public int OrgUnitID { get; set; }

        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }

        [Display(Name = "Cost Center")]
        public int CostCenterID { get; set; }

        [Display(Name = "Superior")]
        public string EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }

        [Display(Name = "Organization Level")]
        public int OrganizationleveId { get; set; }

        [Display(Name = "Begin Effective")]
        public DateTime? BeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? LastEff { get; set; }

        [Display(Name = "Parent Organization")]
        public string OrganizationUnitID { get; set; }
        public string OrganizationUnit { get; set; }

        public bool IsLegalEntity { get; set; }

        [Display(Name = "Legal Entity")]
        public int? LegalEntityInformationID { get; set; }

        public int  CategoryID { get; set; }

        public int StatusID { get; set; }

    }
}