using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UsersImeitransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Imei { get; set; }
        public int DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        public virtual User DeletedByNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
