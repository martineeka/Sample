using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.LocationClassification
{
    public class ListLocationClassificationViewModel
    {
        public string ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Sequence { get; set; }
        public int BusinessGroupId { get; set; }
    }
}
