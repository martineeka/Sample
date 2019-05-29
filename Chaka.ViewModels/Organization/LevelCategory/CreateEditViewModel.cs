using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.LevelCategory
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        
        //[Remote("ValidateLevelCategoryCode", "LevelCategory", "Organization", AdditionalFields = "ID", ErrorMessage = "Level Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character")]
        public string Name { get; set; }


        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Sequence")]
        public int Sequence { get; set; }

        public int MaxSequenceLevel { get; set; }
    }
}
