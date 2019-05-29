using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.BusinessFieldRegulation
{
    public class CreateEditClassificationViewModel
    {
        public string ID { get; set; }
        public string BusinessFieldCategoryID { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Maximum 3 characters")]
        public string MainSectionCode { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Maximum 3 characters")]
        public string SectionCode { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Maximum 3 characters")]
        public string SubSectionCode { get; set; }

        [Required]
        [StringLength(3, ErrorMessage = "Maximum 3 characters")]
        public string GroupCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string MainSectionName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string SectionName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string SubSectionName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Maximum 100 characters")]
        public string GroupName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Maximum 200 character")]
        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
