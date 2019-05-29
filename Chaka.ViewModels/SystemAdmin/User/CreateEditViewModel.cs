using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.User
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage ="Maximal 20 Character")]
        public string UserName { get; set; }
        public string LoginName { get; set; }

        [Required]
        [StringLength(200, ErrorMessage ="Maximal 200 Character")]
        public string Email { get; set; }

        [Required]
        public string EmployeeID { get; set; }
        public string Employee { get; set; }
        public string NIK { get; set; }

        [Required]
        [StringLength(200, ErrorMessage ="Maximal 200 Character")]
        public string Password { get; set; }

        [Required]
        public string EmployeeListFilterID { get; set; }

        [Required]
        public string ResponsibilityGroupID { get; set; }

        [Required]
        public string PreferenceGroupID { get; set; }

        [Required]
        public string RestrictionGroupID { get; set; }

        [Required]
        public string UserStatusID { get; set; }

        public string action { get; set; }

    }
}
