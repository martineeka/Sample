using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.Organization.OrgUnit
{
   public class CreateEditJobTitleViewModel
    {
        public string ID { get; set; }
        public string OrganizationID { get; set; }


        [Display(Name = "JobTitle")]
        public int? JobTitleID { get; set; }

        [Display(Name = "Valid From")]
        public DateTime? ValidFrom { get; set; }

        [Display(Name = "Valid To")]
        public DateTime? ValidTo { get; set; }

    }
}
