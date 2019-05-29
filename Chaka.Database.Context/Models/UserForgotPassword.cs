using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class UserForgotPassword
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid Guid { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
    }
}
