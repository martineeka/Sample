using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.JobFamily
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        [Remote("ValidateJobFamilyCode", "JobFamily", "Organization", AdditionalFields = "ID", ErrorMessage = "JobFamily Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character")]
        public string Name { get; set; }


        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Description { get; set; }
    }
}
