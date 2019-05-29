using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UserStatus
    {
        public UserStatus()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAccessible { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
