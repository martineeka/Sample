using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrgUnitTransDetJobTitle
    {
        public int Id { get; set; }
        public int OrgUnitTransactionDetId { get; set; }
        public string JobtitleId { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int StatusId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual OrgUnitTransactionDetail OrgUnitTransactionDet { get; set; }
    }
}
