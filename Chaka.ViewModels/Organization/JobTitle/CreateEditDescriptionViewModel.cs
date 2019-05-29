using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.JobTitle
{
    public class CreateEditDescriptionViewModel
    {
        public string ID { get; set; }
        public int JobTitleID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 10 characters")]
        [Remote("ValidateDescriptionCode", "JobTitle", "Organization", AdditionalFields = "ID", ErrorMessage = "Job Title Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 character")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum 500 character")]
        public string Description { get; set; }
    }
}
