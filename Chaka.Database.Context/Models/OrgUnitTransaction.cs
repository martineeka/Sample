using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrgUnitTransaction
    {
        public OrgUnitTransaction()
        {
            OrgUnitTransactionDetail = new HashSet<OrgUnitTransactionDetail>();
        }

        public int Id { get; set; }
        public string DecisionNumber { get; set; }
        public string RequestorId { get; set; }
        public int TransactionStatusId { get; set; }
        public string ProcessInstance { get; set; }
        public string Folio { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int OrgUnitId { get; set; }
        public string Reason { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
    }
}
