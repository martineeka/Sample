using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class EmployeeListFilterStatus
    {
        public int Id { get; set; }
        public int EmployeeListFilterId { get; set; }
        public int EmploymentStatusId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual EmployeeListFilter EmployeeListFilter { get; set; }
        public virtual EmployeeStatus EmploymentStatus { get; set; }
    }
}
