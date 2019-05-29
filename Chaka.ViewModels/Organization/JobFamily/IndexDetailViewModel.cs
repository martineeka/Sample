using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.JobFamily
{
    public  class IndexDetailViewModel
    {
        public int JobFamilyID { get; set; }
        public string EncryptJobFamilyID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string[] NotSelected { get; set; }
        public string[] Selected { get; set; }

        public IEnumerable<ListDetailViewModel> List { get; set; }
        public IEnumerable<ListDetailViewModel> ListSelected { get; set; }

    }
}
