using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.GroupGrade
{
     public class GroupGradeDetailViewModel
    {
        public Chaka.Database.Context.Models.GradeGroup GroupGrade { get; set; }
        public IDictionary<Chaka.Database.Context.Models.Function, bool> Functions { get; set; }
        public int GroupGradeID { get; set; }
    }
}
