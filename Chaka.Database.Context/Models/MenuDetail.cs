using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class MenuDetail
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int FunctionId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Function Function { get; set; }
        public virtual Menu Menu { get; set; }
    }
}
