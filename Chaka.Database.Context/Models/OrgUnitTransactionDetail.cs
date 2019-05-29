using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrgUnitTransactionDetail
    {
        public OrgUnitTransactionDetail()
        {
            OrgUnitTransDetJobTitle = new HashSet<OrgUnitTransDetJobTitle>();
        }

        public int Id { get; set; }
        public int OrgUnitTransactionId { get; set; }
        public int OrgUnitId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int OrganizationleveId { get; set; }
        public int? SuperiorId { get; set; }
        public int CostCenterId { get; set; }
        public string Description { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int? ParentId { get; set; }
        public bool IsLegalEntity { get; set; }
        public int? LegalEntityInformationId { get; set; }
        public bool? IsCurrentUsed { get; set; }
        public int BusinessGroupId { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual OrganizationUnitCategory Category { get; set; }
        public virtual CostCenter CostCenter { get; set; }
        public virtual LegalEntityInformation LegalEntityInformation { get; set; }
        public virtual OrgUnitTransaction OrgUnitTransaction { get; set; }
        public virtual OrganizationLevel Organizationleve { get; set; }
        public virtual OrgUnitChangeStatus Status { get; set; }
        public virtual Employee Superior { get; set; }
        public virtual ICollection<OrgUnitTransDetJobTitle> OrgUnitTransDetJobTitle { get; set; }
    }
}
