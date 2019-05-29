using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.BusinessFieldRegulation
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Maximum 10 characters")]
        public string Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 character")]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Maximum 500 character")]
        public string Description { get; set; }

        [Required]
        public DateTime AppointedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
