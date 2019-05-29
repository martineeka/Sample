using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class RespGroupDetail
    {
        public int Id { get; set; }
        public int ResponsibilityGroupId { get; set; }
        public int ResponsibilityId { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Responsibility Responsibility { get; set; }
        public virtual ResponsibilityGroup ResponsibilityGroup { get; set; }
    }
}
