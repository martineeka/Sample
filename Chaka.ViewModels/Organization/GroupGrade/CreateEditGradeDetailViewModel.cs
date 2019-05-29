using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.GroupGrade
{
   public class CreateEditGradeDetailViewModel
    {
        public string ID { get; set; }
        public string GroupGradeID { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("ValidateGradeCode", "GroupGrade", "Organization", AdditionalFields = "ID", ErrorMessage = "Grade code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum of 100 characters")]
        [Remote("ValidateGradeDetailName", "GroupGrade", "Organization", AdditionalFields = "ID", ErrorMessage = "Grade code has been used")]
        public string Name { get; set; }

        [Required]
        public int Sequence { get; set; }

        public int MaxSequence { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maximum of 500 characters")]
        public string Description { get; set; }

    }
}
