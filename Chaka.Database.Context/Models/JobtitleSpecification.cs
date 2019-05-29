using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class JobtitleSpecification
    {
        public JobtitleSpecification()
        {
            JobtitleSpecificationMajor = new HashSet<JobtitleSpecificationMajor>();
        }

        public int Id { get; set; }
        public int JobTitleId { get; set; }
        public int LineId { get; set; }
        public int JobFunctionId { get; set; }
        public int LevelEduId { get; set; }
        public int? MinExp { get; set; }
        public string Descriptions { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual JobFunction JobFunction { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual LineOfBusiness Line { get; set; }
        public virtual ICollection<JobtitleSpecificationMajor> JobtitleSpecificationMajor { get; set; }
    }
}
