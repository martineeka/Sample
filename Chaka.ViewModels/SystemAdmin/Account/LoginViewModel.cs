using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LoginName")]
        public string LoginName { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
