using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class LocationClassification
    {
        public LocationClassification()
        {
            Location = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual ICollection<Location> Location { get; set; }
    }
}
