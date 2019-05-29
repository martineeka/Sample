using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.ViewModels.SystemAdmin.Responsibility
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximal 50 characters required")]
        [Remote("ValidateName", "Responsibility", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Name has been used")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Maximal 500 characters required")]
        public string Description { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Menu")]
        public int MenuID { get; set; }

        [Display(Name = "Business Group")]
        public int DefaultBusinessGroupID { get; set; }
    }
}
