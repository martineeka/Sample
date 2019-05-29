using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Menu
{
    public class IndexDetailViewModel
    {
        public int MenuID { get; set; }
        public string EncryptedMenuID { get; set; } 
        public string NameINA { get; set; }
        public string NameENG { get; set; }

        public string[] NotSelected { get; set; }
        public string[] Selected { get; set; }

        public IEnumerable<ListMenuFunctionViewModel> List { get; set; }
        public IEnumerable<ListMenuFunctionViewModel> ListSelected { get; set; }
    }
}
