using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Configuration
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string HelpText { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
