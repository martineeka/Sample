using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.ResponsibilityGroup
{
    public class ListResponsibilityGroupViewModel
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
