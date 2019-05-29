using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnitChange
{
    public class ListOrgUnitChangeViewModel
    {
        public string ID { get; set; }
        public DateTime TanggalRequest { get; set; }
        public string OrgUnit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
    }
}
