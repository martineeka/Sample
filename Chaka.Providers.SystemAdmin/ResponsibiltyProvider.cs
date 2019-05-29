using Chaka.Database.Context.Models;
using Chaka.ViewModels.SystemAdmin.Responsibility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaka.Providers.SystemAdmin
{
    public interface IResponsibilityService : IDataService<Responsibility>
    {
        IQueryable<ListResponsibiltyViewModel> GetList();
        bool IsUsedByRespGroupDetail(int ID);
    }
    public class ResponsibiltyProvider : IResponsibilityService
    {
        readonly ChakaContext context;

        public ResponsibiltyProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Responsibility> AllResponsibilities
        {
            get
            {
                return context.Responsibility.Where(x => !x.DelDate.HasValue);
            }
        }
        public IQueryable<OrgUnit> AllOrganizationUnits
        {
            get { return context.OrgUnit.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<ListResponsibiltyViewModel> GetList()
        {
            var query = from data in AllResponsibilities
                        join legal in AllOrganizationUnits on data.DefaultBusinessGroupId equals legal.Id
                        select new ListResponsibiltyViewModel()
                        {
                            ID = data.Id.ToString(),
                            Name = data.Name,
                            MenuID = data.MenuId,
                            MenuName = data.Menu.NameEng,
                            DefaultBusinessGroupID = data.DefaultBusinessGroupId,
                            LegalEntityName = legal.Name,
                            IsActive = data.IsActive,
                            Description = data.Description
                        };
            return query;
        }
        public IQueryable<Responsibility> Get() => context.Responsibility.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public Responsibility Get(int Id) => context.Responsibility.AsNoTracking().FirstOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public int Add(Responsibility entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }
        public int Edit(Responsibility entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var responsibility = Get(Id);
            if (responsibility != null)
            {
                context.SbDelete(responsibility);
                context.SaveChanges();
            }
        }

        public bool IsUsedByRespGroupDetail(int ID)
        {
            var result = context.RespGroupDetail.Any(e => e.ResponsibilityId == ID);

            return result;
        }
    }
}
