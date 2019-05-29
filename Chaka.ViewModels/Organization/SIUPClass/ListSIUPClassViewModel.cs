using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.SIUPClass
{
    public class ListSIUPClassViewModel
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? AssetFrom { get; set; }
        public decimal? AssetTo { get; set; }
    }
}
