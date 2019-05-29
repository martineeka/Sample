using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrgUnitChangeStatus
    {
        public OrgUnitChangeStatus()
        {
            OrgUnitTransactionDetail = new HashSet<OrgUnitTransactionDetail>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
    }
}
