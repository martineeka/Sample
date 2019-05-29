using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Browse
{
    public class IndexBrowseSuperiorViewModel
    {
        public string Callback { get; set; }
        public string HeadID { get; set; }
        public IEnumerable<BrowseSuperiorViewModel> List { get; set; }

    }
}
