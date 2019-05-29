using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.OrganizationReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IOrganizationReportService
    {
        IQueryable<ListReportOrganizationChart> GetReport();
        IQueryable<ListReportOrganizationChartTreeView> GetReportTreeView();

        IQueryable<ListReportLogChange> GetList(DateTime startDate, DateTime endDate);
    }
    public class OrganizationReportProvider : IOrganizationReportService
    {
        readonly ChakaContext context;

        public OrganizationReportProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<OrgUnit> AllOrgUnits
        {
            get { return context.OrgUnit.Where(x => !x.DelDate.HasValue); }
        }


        public IQueryable<ListReportOrganizationChart> GetReport()
        {
            var query = from orgChart in AllOrgUnits
                        select new ListReportOrganizationChart()
                        {
                            ID = orgChart.Id,
                            ParentID = orgChart.ParentId,
                            Code = orgChart.Code,
                            Name = orgChart.Name,
                            Description = orgChart.Description,
                            BeginEff = orgChart.BeginEff,
                            LastEff = orgChart.LastEff
                        };

            return query;
        }
        public IQueryable<ListReportLogChange> GetList(DateTime startDate, DateTime endDate)
        {
            var query = from orgChart in context.OrgUnit
                        join emp in context.Employee on orgChart.CreatedBy equals emp.Id
                        join empBio in context.EmployeeBiodata on emp.Id equals empBio.EmployeeId 
                        join empBioUp in context.Employee on orgChart.UpdatedBy equals empBioUp.Id into jointBio
                        from joint1 in jointBio.DefaultIfEmpty()
                        join empBioUp in context.EmployeeBiodata on joint1.Id equals empBioUp.EmployeeId into joinBioUp
                        from joint2 in joinBioUp.DefaultIfEmpty()
                        where startDate >= orgChart.BeginEff && (orgChart.LastEff >= endDate || orgChart.LastEff == null)
                        select new ListReportLogChange()
                        {
                            OrganizationName = orgChart.Name,
                            CreatedBy = empBio.FirstName + " " + empBio.MiddleName + " " + empBio.LastName,
                            CreatedDate = orgChart.CreatedDate,
                            ChangeBy = joint2.FirstName + " " + joint2.MiddleName + " " + joint2.LastName,
                            ChangeDate = orgChart.UpdatedDate,
                            //DeletedBy = joint2.FirstName + " " + joint2.MiddleName + " " + joint2.LastName,
                            DeletedDate = orgChart.DelDate
                        };
            return query;
        }

        public IQueryable<ListReportOrganizationChartTreeView> GetReportTreeView()
        {
            var query = from orgChart in AllOrgUnits
                        select new ListReportOrganizationChartTreeView()
                        {
                            id = orgChart.Id,
                            ParentID = orgChart.ParentId,
                            text = orgChart.Name,
                            Code = orgChart.Code,
                            BeginEff = orgChart.BeginEff,
                            LastEff = orgChart.LastEff
                        };

            return query;
        }
    }
}
