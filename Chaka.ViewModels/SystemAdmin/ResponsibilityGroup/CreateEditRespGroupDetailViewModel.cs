using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.ResponsibilityGroup
{
    public class CreateEditRespGroupDetailViewModel
    {
        public string ID { get; set; }
        //[Remote("ValidateRespGroupCode", "ResponsibilityGroup", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Responsibility group code has been used")]
        [Required]
        public int ResponsibilityID { get; set; }
        public int ResponsibilityGroupID { get; set; }

        public bool IsActive { get; set; }
    }
}
