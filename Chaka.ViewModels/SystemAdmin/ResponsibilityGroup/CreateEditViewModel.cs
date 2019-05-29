using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.ResponsibilityGroup
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }
        [Remote("ValidateRespGroupCode", "ResponsibilityGroup", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Responsibility group code has been used")]
        [Required]
        public string Code { get; set; }
        [Required]

        public string Name { get; set; }

        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
