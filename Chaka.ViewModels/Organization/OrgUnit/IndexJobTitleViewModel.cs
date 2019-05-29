using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
   public class IndexJobTitleViewModel
    {
        public string OrganizationID { get; set; }
        public IEnumerable<ListJobTitleViewModel> ListJobTitle { get; set; }
    }
}
