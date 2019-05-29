using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnitChange
{
    public class ListOrgUnitChangeDetailViewModel
    {
        public string ID { get; set; }
        public int OrgUnitID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CostCenter { get; set; }
        public string Description { get; set; }
        public string Superior { get; set; }
        public string OrganizationLevel { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public string ParentOrgUnit { get; set; }
        public string Category { get; set; }
        public string IsLegalEntity { get; set; }
        public string Status { get; set; }
    }
}