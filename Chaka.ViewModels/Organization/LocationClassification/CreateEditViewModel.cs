using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.LocationClassification
{
    public class CreateEditViewModel
    {
        public string ID { get; set; } 

        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 Characters")]
        [Remote("ValidateLocationClassificationCode", "Location Classification", "Organization", AdditionalFields = "ID", ErrorMessage = "Location Classification Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 Characters")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string Description { get; set; }

        public int Sequence { get; set; }

        public int MaxSequenceLocationClassification { get; set; }

        public int LevelCategoryID { get; set; }


    }
}
