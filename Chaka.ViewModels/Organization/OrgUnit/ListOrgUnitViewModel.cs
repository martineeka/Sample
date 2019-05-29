using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
    public class ListOrgUnitViewModel
    {
        public string ID { get; set; } 
        public string Code { get; set; }
        public string ParentOrgUnit { get; set; } 
        public string CostCenterName { get; set; }

        public string SuperiorName { get; set; }

        public string Name { get; set; }
        public string OrganizationUnitLevel { get; set; }
        //public string LocationName { get; set; }
        public string Category { get; set; }
        public bool IsLegalEntity { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public string Description { get; set; }
    }
}
