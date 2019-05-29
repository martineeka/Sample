using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnitChange
{
    public class IndexDetailViewModel
    {
        public int ID { get; set; }
        public string TanggalRequest { get; set; }
        public string OrgUnit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
    }
}
