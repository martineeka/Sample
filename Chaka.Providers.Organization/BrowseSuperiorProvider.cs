using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IBrowseSuperiorProviderService
    {
        IEnumerable<BrowseSuperiorViewModel> GetList();
        Employee GetSelectedEmployee(int employeeID);
        string GetCurrentEmployeeFullName(int employeeID);

    }
    public class BrowseSuperiorProvider : IBrowseSuperiorProviderService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;

        public BrowseSuperiorProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Employee> AllSuperiors()
        {
            return context.Employee.Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        }

        public IQueryable<EmployeeBiodata> AllEmployeeBiodatas
        {
            get { return context.EmployeeBiodata.Where(orgUnit => !orgUnit.DelDate.HasValue && (orgUnit.BeginEff < DateTime.Now && orgUnit.LastEff == null || orgUnit.LastEff >= DateTime.Now)); }
        }

      

        public IEnumerable<BrowseSuperiorViewModel> GetList()
        {
            var query = from sup in AllSuperiors()
                        join emp in AllEmployeeBiodatas on sup.Id equals emp.EmployeeId
                        select new BrowseSuperiorViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(sup.Id)),
                            NIK = sup.Nik,
                            Name =
                                emp.MiddleName == null
                                ? emp.LastName == null
                                    ? string.Format("{0}", emp.FirstName)
                                    : string.Format("{0} {1} ", emp.FirstName,
                                     emp.LastName)
                                    : string.Format("{0} {1} {2} ", emp.FirstName,
                                emp.MiddleName, emp.LastName)
                        };

            return query;
        }

        public IQueryable<Employee> AllSuperior
        {
            get { return context.Employee.Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue); }
        }
        public Employee GetSelectedEmployee(int employeeID)
        {
            var query = AllSuperior.SingleOrDefault(employee => employee.Id == employeeID);
            return query;
        }

        public string GetCurrentEmployeeFullName(int employeeID)
        {
            var fullName = string.Empty;
            var query = GetCurrentSelectedEmployeeBiodata(employeeID);
            if (query != null)
            {
                fullName = string.IsNullOrEmpty(query.MiddleName)
                                  ? string.IsNullOrEmpty(query.LastName)
                                      ? string.Format("{0}", query.FirstName)
                                      : string.Format("{0} {1} ", query.FirstName,
                                          query.LastName)
                                  : string.Format("{0} {1} {2} ",  query.FirstName,
                                      query.MiddleName, query.LastName);
            }

            return fullName;
        }

        public EmployeeBiodata GetCurrentSelectedEmployeeBiodata(int employeeID)
        {
            var query = AllEmployeeBiodatas
                .SingleOrDefault(biodata => biodata.EmployeeId == employeeID);
            return query;
        }

     

    }
}
