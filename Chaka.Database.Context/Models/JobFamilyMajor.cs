﻿using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class JobFamilyMajor
    {
        public int Id { get; set; }
        public int? MajorId { get; set; }
        public int? JobFamilyId { get; set; }
        public int? BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual JobFamily JobFamily { get; set; }
    }
}
