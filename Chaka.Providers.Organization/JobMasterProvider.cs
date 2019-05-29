using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.JobMaster;
using Microsoft.EntityFrameworkCore;

namespace Chaka.Providers.Organization
{
    public interface IJobMasterService : IDataService<JobMaster>
    {
        IQueryable<ListJobMasterViewModel> GetList();
        AjaxViewModel validateCombination(CreateEditViewModel model);

        bool IsJobMasterNameValid(string name, string id);
        bool IsJobMasterCodeValid(string code, string id);
    }

    public class JobMasterProvider:IJobMasterService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;

        public JobMasterProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<JobMaster> AllJobMasters
        {
            get { return context.JobMaster.Where(x => !x.DelDate.HasValue); }
        }
        public IQueryable<JobMaster> Get() => context.JobMaster.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public JobMaster Get(int Id) => context.JobMaster.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);


        public int Add(JobMaster entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var menu = Get(Id);
            if (menu != null)
            {
                context.SbDelete(menu);
                context.SaveChanges();
            }
        }

        public int Edit(JobMaster entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IQueryable<ListJobMasterViewModel> GetList()
        {
            var query = from data in AllJobMasters
                        select new ListJobMasterViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            Description = data.Description
                        };
            return query;
        }

        public bool IsJobMasterCodeValid(string code, string id)
        {
            var result = AllJobMasters.Any(JM => JM.Code == code && JM.Id != Convert.ToInt32(id));
            return result;
        }

        public bool IsJobMasterNameValid(string name, string id)
        {
            var result = AllJobMasters.Any(JM => JM.Name == name && JM.Id != Convert.ToInt32(id));
            return result;
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.JobMaster.Any(lv =>
                lv.Code.ToLower() == model.Code.ToLower()
                && (lv.Id != id)
                && !lv.DelDate.HasValue
                && lv.BusinessGroupId == businessGroupID);

            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Code has been used";
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
