using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class KppBranch
    {
        public KppBranch()
        {
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual CityDeprecated City { get; set; }
        public virtual CountryDeprecated Country { get; set; }
        public virtual State State { get; set; }
        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
