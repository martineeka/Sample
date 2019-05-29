using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UserResponsibility
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int ResponsibilityId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Responsibility Responsibility { get; set; }
        public virtual User User { get; set; }
    }
}
