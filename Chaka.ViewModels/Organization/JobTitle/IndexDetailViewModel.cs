using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.JobTitle
{
    public class IndexDetailViewModel
    {

        public int ID { get; set; }
        public int JobTitleID { get; set; }
        public string EncryptedJobTitleID { get; set; }
        public string JobTitleCode { get; set; }
        public string JobTitleName { get; set; }
        public string JobStatus { get; set; }
        public string JobLevel { get; set; }
        public string JobLevelCategory { get; set; }
        public string Description { get; set; }
        public string BeginEff { get; set; }
        public string LastEff { get; set; }
        public string[] NotSelected { get; set; }
        public string[] Selected { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListDetail { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListUnSelected { get; set; }
        public IEnumerable<ListDescriptionViewModel> ListSelected { get; set; }

    }
}
