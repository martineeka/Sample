using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Responsibility
    {
        public Responsibility()
        {
            RespGroupDetail = new HashSet<RespGroupDetail>();
            UserResponsibility = new HashSet<UserResponsibility>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MenuId { get; set; }
        public int DefaultBusinessGroupId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual ICollection<RespGroupDetail> RespGroupDetail { get; set; }
        public virtual ICollection<UserResponsibility> UserResponsibility { get; set; }
    }
}
