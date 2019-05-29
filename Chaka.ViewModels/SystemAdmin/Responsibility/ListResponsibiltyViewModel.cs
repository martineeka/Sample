using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Responsibility
{
    public class ListResponsibiltyViewModel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public bool FullAccess { get; set; }
        public bool IsActive { get; set; }
        public int MenuID { get; set; }
        public string MenuName { get; set; }
        public int DefaultBusinessGroupID { get; set; }
        public string LegalEntityName { get; set; }
        public string Description { get; set; }
    }
}
