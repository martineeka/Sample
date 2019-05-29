using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrganizationReport
{
    public class ListReportLogChange
    {
        public string OrganizationName { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ChangeBy { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
