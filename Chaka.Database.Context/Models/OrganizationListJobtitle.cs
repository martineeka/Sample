using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class OrganizationListJobtitle
    {
        public int Id { get; set; }
        public int? JobTitleId { get; set; }
        public int? OrganizationId { get; set; }
        public DateTime? BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual JobTitle JobTitle { get; set; }
        public virtual OrgUnit Organization { get; set; }
    }
}
