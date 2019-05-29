using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.BusinessFieldRegulation
{
    public class ListBusinessFieldClassificationViewModel
    {
        public string ID { get; set; }
        public string BusinessFieldCategoryID { get; set; }
        public string MainSectionCode { get; set; }
        public string SectionCode { get; set; }
        public string SubSectionCode { get; set; }
        public string GroupCode { get; set; }
        public string MainSectionName { get; set; }
        public string SectionName { get; set; }
        public string SubSectionName { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
