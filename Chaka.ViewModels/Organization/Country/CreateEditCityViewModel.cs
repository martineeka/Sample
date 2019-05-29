using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.Country
{
    public class CreateEditCityViewModel
    {
        public string ID { get; set; }
        public string ProvinceID { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
