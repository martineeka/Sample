﻿using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class LevelCategory
    {
        public LevelCategory()
        {
            Level = new HashSet<Level>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<Level> Level { get; set; }
    }
}
