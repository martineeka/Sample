using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.JobTitle
{
    public class CreateEditSpecificationViewModel
    {
        public string ID { get; set; }
        public string JobTitleID { get; set; }
        public int LineID { get; set; }
        public int JobFunctionID { get; set; }
        public int LevelEduID { get; set; }
        public int? MinExp { get; set; }
        public string Descriptions { get; set; }
        public string[] NotSelected { get; set; }
        public string[] Selected { get; set; }
        public string EncryptedJobTitleID { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListUnSelected { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListSelected { get; set; }
    }
}
