using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeInfoRestrictionGroup
{
    public class CreateEditViewModel
    {
        public string ID { get; set; }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool IsActive { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public bool BiodataAuthority { get; set; }
        public bool PaymentAuthority { get; set; }
        public bool PayrollAuthority { get; set; }
        public bool CustomAuthority { get; set; }
        public bool SkillAuthority { get; set; }
        public bool FamilyAuthority { get; set; }
        public bool EducationAuthority { get; set; }
        public bool GradeAuthority { get; set; }
        public bool JobTitleAuthority { get; set; }
        public bool OrgUnitAuthority { get; set; }
        public bool StatusAuthority { get; set; }
        public bool SalaryAuthority { get; set; }
        public bool LocationAuthority { get; set; }
        
    }
}
