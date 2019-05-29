using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Menu
{
    public class ListMenuViewModel
    {
        public string ID { get; set; }
        public string NameINA { get; set; }
        public string NameENG { get; set; }
        public string Description { get; set; }
    }
    public class FunctionViewModel {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class MenuViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<FunctionViewModel> Function { get; set; }

    }
    public class SideMenuViewModel
    {
        public List<MenuViewModel> Menu { get; set; }

    }
}
