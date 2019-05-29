using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Chaka.ViewModels.SystemAdmin.Menu
{
    public class CreateMenuDetailViewModel
    {
        public string ID { get; set; }

        [Required]
        public string MenuID { get; set; }

        [Required]
        public string FunctionID { get; set; }

        
    }
}
