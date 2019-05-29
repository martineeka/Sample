using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
   public class ListJobTitleViewModel
    {
        public string OrganizationUnitID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
