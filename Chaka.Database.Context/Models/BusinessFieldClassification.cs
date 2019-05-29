using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class BusinessFieldClassification
    {
        public BusinessFieldClassification()
        {
            LegalEntityInformation = new HashSet<LegalEntityInformation>();
        }

        public int Id { get; set; }
        public int BusinessFieldCategoryId { get; set; }
        public string MainSectionCode { get; set; }
        public string SectionCode { get; set; }
        public string SubSectionCode { get; set; }
        public string GroupCode { get; set; }
        public string MainSectionName { get; set; }
        public string SectionName { get; set; }
        public string SubSectionName { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual BusinessFieldCategory BusinessFieldCategory { get; set; }
        public virtual ICollection<LegalEntityInformation> LegalEntityInformation { get; set; }
    }
}
