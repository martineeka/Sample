using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels.Browse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    //class BrowseOrganizationUnitProvider
    public interface IBrowseOrganizationUnitProviderService 
    {
        IEnumerable<BrowseOrganizationUnitViewModel> GetList();
    }

    public class BrowseOrganizationUnitProvider : IBrowseOrganizationUnitProviderService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;

        public BrowseOrganizationUnitProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<OrgUnit> AllOrgUnit()
        {
            return context.OrgUnit.Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        }

        public IEnumerable<BrowseOrganizationUnitViewModel> GetList()
        {
            var query = from org in AllOrgUnit()
                        select new BrowseOrganizationUnitViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(org.Id)),
                            Code = org.Code,
                            Name = org.Name,
                            //Location = org.Location.Name,
                            CostCenter = org.CostCenter.Name,
                            Description = org.Description
                        };

            return query;
        }
    }
}
