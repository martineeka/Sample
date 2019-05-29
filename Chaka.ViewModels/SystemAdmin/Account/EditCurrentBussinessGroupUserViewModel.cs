using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Account
{
    public class EditCurrentBussinessGroupUserViewModel
    {
        public string ID { get; set; }
        [Required]
        public int CurrentBussinessGroupID { get; set; }
    }
}
