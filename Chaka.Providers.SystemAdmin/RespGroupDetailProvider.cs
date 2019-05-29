using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.SystemAdmin.ResponsibilityGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{
    public interface IRespGroupDetailService : IDataService<RespGroupDetail>
    {
        AjaxViewModel validateIsAxist(CreateEditRespGroupDetailViewModel model);
    }

    public class RespGroupDetailProvider : IRespGroupDetailService
    {
        ChakaContext context;

        public RespGroupDetailProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<RespGroupDetail> AllRespGroupDetail
        {
            get { return context.RespGroupDetail.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<RespGroupDetail> Get() => AllRespGroupDetail.AsNoTracking();

        public RespGroupDetail Get(int id) => AllRespGroupDetail.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public int Add(RespGroupDetail entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }
        public int Edit(RespGroupDetail entity)
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

        public AjaxViewModel validateIsAxist(CreateEditRespGroupDetailViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var exist = AllRespGroupDetail.Any(lv =>
                lv.ResponsibilityGroupId == model.ResponsibilityGroupID
                && (lv.Id != id)
                && (lv.ResponsibilityId == model.ResponsibilityID)
                && !lv.DelDate.HasValue);

            if (exist)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = " Responsibility already exists ";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }
    }
}
