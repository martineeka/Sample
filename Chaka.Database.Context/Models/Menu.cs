using System;
using System.Collections.Generic;

namespace Chaka.Database.Context.Models
{
    public partial class Menu
    {
        public Menu()
        {
            MenuDetail = new HashSet<MenuDetail>();
            Responsibility = new HashSet<Responsibility>();
        }

        public int Id { get; set; }
        public string NameIna { get; set; }
        public string Description { get; set; }
        public int BusinessGroupId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DelDate { get; set; }
        public string NameEng { get; set; }

        public virtual ICollection<MenuDetail> MenuDetail { get; set; }
        public virtual ICollection<Responsibility> Responsibility { get; set; }
    }
}
