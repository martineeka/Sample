﻿using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class CountryDeprecated
    {
        public CountryDeprecated()
        {
            KppBranch = new HashSet<KppBranch>();
            Location = new HashSet<Location>();
            ProvinceDeprecated = new HashSet<ProvinceDeprecated>();
            State = new HashSet<State>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
        public string Description { get; set; }

        public virtual ICollection<KppBranch> KppBranch { get; set; }
        public virtual ICollection<Location> Location { get; set; }
        public virtual ICollection<ProvinceDeprecated> ProvinceDeprecated { get; set; }
        public virtual ICollection<State> State { get; set; }
    }
}