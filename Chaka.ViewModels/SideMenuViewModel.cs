using System;
using System.Collections.Generic;
using System.Text;
using Chaka.Database.Context.Models;

namespace Chaka.ViewModels
{
    public class SideMenuViewModel
    {
        public IList<Responsibility> Responsibilities { get; set; }
        public MenuDetail RequestedFunction { get; set; }

        public string SearchNameMenu { get; set; }
    }
}
