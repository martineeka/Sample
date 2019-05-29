using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Chaka.ViewModels.SystemAdmin.Menu
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("ValidateMenuNameIna", "Menu", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Menu Name has been used")]
        public string NameINA { get; set; }

        [Required]
        [StringLength(100)]
        [Remote("ValidateMenuNameEng", "Menu", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Menu Name has been used")]
        public string NameENG { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maximum of 500 characters")]
        public string Description { get; set; }
    }
}
