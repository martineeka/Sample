using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.BusinessFieldRegulation
{
    public class IndexBusinessFieldRegulationViewModel
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AppointedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
