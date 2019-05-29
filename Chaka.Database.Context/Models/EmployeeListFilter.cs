using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class EmployeeListFilter
    {
        public EmployeeListFilter()
        {
            EmployeeListFilterGrade = new HashSet<EmployeeListFilterGrade>();
            EmployeeListFilterLevel = new HashSet<EmployeeListFilterLevel>();
            EmployeeListFilterLocation = new HashSet<EmployeeListFilterLocation>();
            EmployeeListFilterOrgUnit = new HashSet<EmployeeListFilterOrgUnit>();
            EmployeeListFilterPosition = new HashSet<EmployeeListFilterPosition>();
            EmployeeListFilterStatus = new HashSet<EmployeeListFilterStatus>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual CoreFilter CoreFilter { get; set; }
        public virtual ICollection<EmployeeListFilterGrade> EmployeeListFilterGrade { get; set; }
        public virtual ICollection<EmployeeListFilterLevel> EmployeeListFilterLevel { get; set; }
        public virtual ICollection<EmployeeListFilterLocation> EmployeeListFilterLocation { get; set; }
        public virtual ICollection<EmployeeListFilterOrgUnit> EmployeeListFilterOrgUnit { get; set; }
        public virtual ICollection<EmployeeListFilterPosition> EmployeeListFilterPosition { get; set; }
        public virtual ICollection<EmployeeListFilterStatus> EmployeeListFilterStatus { get; set; }
    }
}
