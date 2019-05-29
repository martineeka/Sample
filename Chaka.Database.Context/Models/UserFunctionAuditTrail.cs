using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UserFunctionAuditTrail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FunctionId { get; set; }
        public int VisitTime { get; set; }
        public DateTime LastVisit { get; set; }
        public int BusinessGroupId { get; set; }

        public virtual Function Function { get; set; }
        public virtual User User { get; set; }
    }
}
