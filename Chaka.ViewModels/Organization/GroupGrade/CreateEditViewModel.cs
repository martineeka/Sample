using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.GroupGrade
{
     public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("ValidateGroupGradeCode", "GroupGrade", "Organization", AdditionalFields = "ID", ErrorMessage = "Group Grade code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maximum of 500 characters")]
        public string Description { get; set; }
    }
}
