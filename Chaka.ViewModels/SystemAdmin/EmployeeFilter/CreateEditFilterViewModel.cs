using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.EmployeeFilter
{
    public class CreateEditFilterViewModel
    {
        //ID
        //EmployeeListFilterID
        //CoreID
        //DetailID

        public string ID { get; set; }

        [Required]
        public int EmployeeListFilterID { get; set; }

        [Required]
        public int CoreID { get; set; }

        [Required]
        public int DetailID { get; set; }


    }
}
