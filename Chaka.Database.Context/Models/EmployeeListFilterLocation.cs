using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class EmployeeListFilterLocation
    {
        public int Id { get; set; }
        public int EmployeeListFilterId { get; set; }
        public int LocationId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual EmployeeListFilter EmployeeListFilter { get; set; }
        public virtual Location Location { get; set; }
    }
}
