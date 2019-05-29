using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.JobMaster
{
   public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(10)]
        [Remote("ValidateJobMasterCode", "JobMaster", "Organization", AdditionalFields = "ID", ErrorMessage = "Job Master Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("ValidateJobMasterName", "JobMaster", "Organization", AdditionalFields = "ID", ErrorMessage = "Job Master Name has been used")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maximum of 500 characters")]
        public string Description { get; set; }
    }
}
