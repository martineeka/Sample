using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class JobTitle
    {
        public JobTitle()
        {
            JobDescription = new HashSet<JobDescription>();
            JobtitleRequirement = new HashSet<JobtitleRequirement>();
            JobtitleSpecification = new HashSet<JobtitleSpecification>();
            OrganizationListJobtitle = new HashSet<OrganizationListJobtitle>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? JobStatusId { get; set; }
        public int? JobLevelId { get; set; }
        public int? JobLevelCategoryId { get; set; }
        public int? JobFamilyId { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual JobStatus JobStatus { get; set; }
        public virtual ICollection<JobDescription> JobDescription { get; set; }
        public virtual ICollection<JobtitleRequirement> JobtitleRequirement { get; set; }
        public virtual ICollection<JobtitleSpecification> JobtitleSpecification { get; set; }
        public virtual ICollection<OrganizationListJobtitle> OrganizationListJobtitle { get; set; }
    }
}
