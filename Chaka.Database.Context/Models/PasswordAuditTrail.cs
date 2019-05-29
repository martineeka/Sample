using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class PasswordAuditTrail
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Counter { get; set; }
        public string Password { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual User User { get; set; }
    }
}
