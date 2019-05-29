using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class State
    {
        public State()
        {
            CityDeprecated = new HashSet<CityDeprecated>();
            KppBranch = new HashSet<KppBranch>();
        }

        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal? MinimumSalary { get; set; }
        public bool IsActive { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }

        public virtual CountryDeprecated Country { get; set; }
        public virtual ICollection<CityDeprecated> CityDeprecated { get; set; }
        public virtual ICollection<KppBranch> KppBranch { get; set; }
    }
}
