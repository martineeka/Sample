﻿using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class JobFamily
    {
        public JobFamily()
        {
            JobFamilyMajor = new HashSet<JobFamilyMajor>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<JobFamilyMajor> JobFamilyMajor { get; set; }
    }
}
