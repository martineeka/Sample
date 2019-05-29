using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrgUnit
    {
        public OrgUnit()
        {
            InverseParent = new HashSet<OrgUnit>();
            OrganizationListJobtitle = new HashSet<OrganizationListJobtitle>();
            OrganizationListLocation = new HashSet<OrganizationListLocation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrganizationlevelId { get; set; }
        public int? SuperiorId { get; set; }
        public int CostCenterId { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int? ParentId { get; set; }
        public bool IsLegalEntity { get; set; }
        public int? LegalEntityInformationId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual CostCenter CostCenter { get; set; }
        public virtual LegalEntityInformation LegalEntityInformation { get; set; }
        public virtual OrgUnit Parent { get; set; }
        public virtual Employee Superior { get; set; }
        public virtual ICollection<OrgUnit> InverseParent { get; set; }
        public virtual ICollection<OrganizationListJobtitle> OrganizationListJobtitle { get; set; }
        public virtual ICollection<OrganizationListLocation> OrganizationListLocation { get; set; }
    }
}
