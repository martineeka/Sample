using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrganizationUnitCategory
    {
        public OrganizationUnitCategory()
        {
            OrgUnitTransactionDetail = new HashSet<OrgUnitTransactionDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<OrgUnitTransactionDetail> OrgUnitTransactionDetail { get; set; }
    }
}
