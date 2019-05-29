using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Menu
{
    public class MenuDetailViewModel
    {
        public Chaka.Database.Context.Models.Menu Menu { get; set; }
        public IDictionary<Chaka.Database.Context.Models.Function, bool> Functions { get; set; }
        public int MenuID { get; set; }
    }
}
