using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class JobDescription
    {
        public int Id { get; set; }
        public int JobtitleId { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
        public int JobMasterId { get; set; }

        public virtual JobTitle Jobtitle { get; set; }
    }
}
