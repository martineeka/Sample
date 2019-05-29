using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.JobTitle
{
    public class IndexNewPageViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 10 characters")]
        [Remote("ValidateJobTitleCode", "JobTitle", "Organization", AdditionalFields = "ID", ErrorMessage = "Job Title Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 character")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum 500 character")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Job Status")]
        public int? JobStatusID { get; set; }

        [Required]
        [Display(Name = "Job Level")]
        public int? JobLevelID { get; set; }

        [Required]
        [Display(Name = "Job Family")]
        public int? JobFamilyID { get; set; }

        [Display(Name = "Job Level Category")]
        public int? JobLevelCategoryID { get; set; }
        public string JobLevelCategory { get; set; }

        [Required]
        [Display(Name = "Begin Effective")]
        public DateTime? BeginEff { get; set; }

        [Display(Name = "Last Effective")]
        public DateTime? LastEff { get; set; }

        public string[] NotSelectedDescription { get; set; }
        public string[] SelectedDescription { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListUnSelectedDescription { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListSelectedDescription { get; set; }
        public string[] NotSelectedMajor { get; set; }
        public string[] SelectedMajor { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListUnSelectedMajor { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListSelectedMajor { get; set; }
    }
}
