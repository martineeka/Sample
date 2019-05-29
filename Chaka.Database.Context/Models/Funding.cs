using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Funding
    {
        public Funding()
        {
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
