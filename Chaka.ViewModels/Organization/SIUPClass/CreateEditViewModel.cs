using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.SIUPClass
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        //[Remote(action: "ValidateLevelCode", controller: "Level", areaName: "Organization", AdditionalFields = "ID", ErrorMessage = "Level Code has been used jul")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Maximum 500 characters")]
        public string Description { get; set; }

        public decimal AssetFrom { get; set; }
        public decimal AssetTo { get; set; }
        public bool IsActive { get; set; }
    }
}
