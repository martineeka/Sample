using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UsersImei
    {
        public int UserId { get; set; }
        public string Imei { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual User User { get; set; }
    }
}
