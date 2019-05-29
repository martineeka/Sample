using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Browse
{
    public class BrowseEmployeeViewModel
    {
        public string ID { get; set; }
        public string NIK { get; set; }
        public string Employee { get; set; }
        //public string OrganizationLevel { get; set; }
        public string Gender { get; set; }
        //public string Superior { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
