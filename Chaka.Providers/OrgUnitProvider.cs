using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.OrgUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers
{
    public interface IOrgUnitService
    {
        IQueryable<ListOrgUnitViewModel> GetList();

    }
    public class OrgUnitProvider : IOrgUnitService
    {

        readonly ChakaContext context;

        public OrgUnitProvider(ChakaContext context)
        {
            this.context = context;
        }
        public IQueryable<OrgUnit> AllOrgUnits
        {
            get
            {
                //return
                //    context.OrgUnit.Where(
                //        orgUnit => !orgUnit.DelDate.HasValue && (orgUnit.BeginEff < DateTime.Now && orgUnit.LastEff == null || orgUnit.LastEff >= DateTime.Now) /*&& orgUnit.BusinessGroupID == CurrentBusinessGroupId*/);
                return context.OrgUnit.Where(x => !x.DelDate.HasValue);
            }
        }

        public IQueryable<ListOrgUnitViewModel> GetList()
        {
            var query = from data in AllOrgUnits
                        select new ListOrgUnitViewModel()
                        {
                            ID = data.Id.ToString(),
                            Name = data.Name,
                            Description = data.Description
                        };
            return query;
        }

    }
}
