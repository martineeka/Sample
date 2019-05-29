using System;
using System.Collections.Generic;
using System.Text;

namespace Chaka.ViewModels.Organization.OrganizationReport
{
    public class ReportOrganizationChart
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }

        public IList<ReportOrganizationChart> items { get; set; } = new List<ReportOrganizationChart>();
    }
    public class ReportOrganizationChartTreeView
    {
        public int id { get; set; }
        public int? ParentID { get; set; }
        public string text { get; set; }
        public string Code { get; set; }

        public DateTime BeginEff { get; set; }
        public DateTime? LastEff { get; set; }
        public IList<ReportOrganizationChartTreeView> items { get; set; } = new List<ReportOrganizationChartTreeView>();
    }

}
