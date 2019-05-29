using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class BusinessFieldRegulation
    {
        public BusinessFieldRegulation()
        {
            BusinessFieldCategory = new HashSet<BusinessFieldCategory>();
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AppointedDate { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<BusinessFieldCategory> BusinessFieldCategory { get; set; }
        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
