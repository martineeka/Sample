using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class CreateEditViewModel
    { 
        public string ID { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        [Remote("ValidateEmployeeListFilterCode", "EmployeeFilter", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Code has been used")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum 500 characters")]
        public string Description { get; set; }
    }
}
