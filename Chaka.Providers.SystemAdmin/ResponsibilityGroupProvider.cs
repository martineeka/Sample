using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.SystemAdmin.ResponsibilityGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaka.Providers.SystemAdmin
{
    public interface IResponsibilityGroupService : IDataService<ResponsibilityGroup>
    {
        IQueryable<ListResponsibilityGroupViewModel> GetList();
        AjaxViewModel validateCombination(CreateEditViewModel model);
        IQueryable<ListRespGroupDetailViewModel> ListRespGroupDetail(int resGroupID);
        bool IsUsedByUser(int ID);
    }
    public class ResponsibilityGroupProvider : IResponsibilityGroupService
    {
        readonly ChakaContext context;

        public ResponsibilityGroupProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<ResponsibilityGroup> AllResponsibilityGroups
        {
            get { return context.ResponsibilityGroup.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<RespGroupDetail> AllRespGroupDetails
        {
            get { return context.RespGroupDetail.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<ResponsibilityGroup> Get() => AllResponsibilityGroups.AsNoTracking();
        public ResponsibilityGroup Get(int Id) => AllResponsibilityGroups.AsNoTracking().FirstOrDefault(x => x.Id == Id);
        public IQueryable<ListResponsibilityGroupViewModel> GetList()
        {
            var query = from data in AllResponsibilityGroups
                        select new ListResponsibilityGroupViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            IsActive = data.IsActive,
                            Description = data.Description
                        };
            return query;
        }

        public IQueryable<ListRespGroupDetailViewModel> ListRespGroupDetail(int resGroupID)
        {
            var query = from respGroupDetail in AllRespGroupDetails
                        join responsibility in context.Responsibility
                        on respGroupDetail.ResponsibilityId equals responsibility.Id into a
                        from left in a.DefaultIfEmpty()
                        where respGroupDetail.ResponsibilityGroupId == resGroupID
                        select new ListRespGroupDetailViewModel()
                        {
                            ID = respGroupDetail.Id.ToString(),
                            Responsibility = left.Name
                        };
            return query;
        }

        public int Add(ResponsibilityGroup entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }
        public int Edit(ResponsibilityGroup entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var responsibilityGroup = Get(Id);
            if (responsibilityGroup != null)
            {
                context.SbDelete(responsibilityGroup);
                context.SaveChanges();
            }
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = AllResponsibilityGroups.Any(lv =>
                lv.Code.ToLower() == model.Code.ToLower()
                && (lv.Id != id)
                && !lv.DelDate.HasValue);

            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = " Code has been used ";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        public bool IsUsedByUser(int ID)
        {
            var result = context.User.Any(e => e.ResponsibilityGroupId == ID);

            return result;
        }
    }
}
