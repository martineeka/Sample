using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class EmployeeInfoRestrictionGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
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
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
    }
}
