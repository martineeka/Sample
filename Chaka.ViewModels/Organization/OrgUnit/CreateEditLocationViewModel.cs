using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
    public class CreateEditLocationViewModel
    {
        public string ID { get; set; }
        public string OrganizationID { get; set; }

        [Display(Name = "Location")]
        public int? LocationID { get; set; }

        [Display(Name = "Valid From")]
        public DateTime? ValidFrom { get; set; }

        [Display(Name = "Valid To")]
        public DateTime? ValidTo { get; set; }
    }
}
