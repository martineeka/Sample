using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
   public class IndexLocationViewModel
    {
        public string OrganizationID { get; set; }
        public IEnumerable<ListLocationDetailViewModel> ListLocation{ get; set; }
    }
}
