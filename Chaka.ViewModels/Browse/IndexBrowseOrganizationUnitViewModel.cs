using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Browse
{
    public class IndexBrowseOrganizationUnitViewModel
    {
        public string Callback { get; set; }
        public string HeadID { get; set; }
        public IEnumerable<BrowseOrganizationUnitViewModel> List { get; set; }

    }
}
