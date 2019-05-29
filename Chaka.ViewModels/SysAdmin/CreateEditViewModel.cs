using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SysAdmin
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(20)]
        [RegularExpression(@"^[^\\]+$", ErrorMessage = "User Name can't contain '\\' character")]
        //[Remote("ValidateUserName", "User", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "User Name has been used")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "LoginName")]
        [StringLength(20)]
        //[Remote("ValidateLoginName", "User", "SystemAdmin", AdditionalFields = "ID", ErrorMessage = "Login Name has been used")]
        public string LoginName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(20, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,20}$", ErrorMessage = "contain at least 8 length, number, special character")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Enter Confirm Password")]
        //[Remote("ValidatePassword", "User", "SystemAdmin", AdditionalFields = "Password", ErrorMessage = "Password not match")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "Password Expire")]
        //public DateTime PasswordExpiration { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public string PersonID { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public string PersonName { get; set; }
        public string PersonCode { get; set; }
        public int EmployeeID { get; set; }
        public int CurrentBusinessGroupID { get; set; }
        public string FullName { get; set; }
        public string NIK { get; set; }
        public int LocationID { get; set; }
        public string Location { get; set; }

        [Required]
        [Display(Name = "Filter")]
        public int EmployeeListFilterID { get; set; }
        [Display(Name = "Responsibility Group")]
        public int? ResponsibilityGroupID { get; set; }

        [Display(Name = "Status")]
        public int UserStatusID { get; set; }
    }
}
