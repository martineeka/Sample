using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OauthClient
    {
        public string Id { get; set; }
        public string Secret { get; set; }
        public string Name { get; set; }
        public int? ApplicationType { get; set; }
        public bool? Active { get; set; }
        public string AllowedOrigin { get; set; }
    }
}
