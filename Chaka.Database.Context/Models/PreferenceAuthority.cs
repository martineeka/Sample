using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class PreferenceAuthority
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FunctionPreferenceId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual Function FunctionPreference { get; set; }
        public virtual User User { get; set; }
    }
}
