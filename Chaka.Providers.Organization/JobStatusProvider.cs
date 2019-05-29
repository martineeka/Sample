using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.JobStatus;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IJobStatusService : IDataService<JobStatus>
    {
        IQueryable<ListJobStatusViewModel> GetList();
        bool IsJobStatusCodeValid(string code, int id);
        AjaxViewModel ValidateCombination(CreateEditViewModel model);
    }
    public class JobStatusProvider : IJobStatusService
    {
        readonly ChakaContext context;
        public JobStatusProvider(ChakaContext context)
        {
            this.context = context;
        }
        public int Add(JobStatus entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(JobStatus entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var jobStatus = Get(Id);
            if (jobStatus != null)
            {
                context.SbDelete(jobStatus);
                context.SaveChanges();
            }
        }

        public IQueryable<JobStatus> Get() => context.JobStatus.AsNoTracking().Where(x => !x.DelDate.HasValue);


        public JobStatus Get(int Id) => context.JobStatus.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<JobStatus> AllJobStatus
        {
            get { return context.JobStatus.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<ListJobStatusViewModel> GetList()
        {
            var query = from jobstatus in AllJobStatus
                        select new ListJobStatusViewModel()
                        {
                            ID = jobstatus.Id.ToString(),
                            Code = jobstatus.Code,
                            Name = jobstatus.Name,
                            Description = jobstatus.Description,

                        };
            return query;
        }

        public bool IsJobStatusCodeValid(string code, int id)
        {
            var result = AllJobStatus.Any(x => x.Code == code && x.Id != id);
            return result;
        }

        public AjaxViewModel ValidateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.JobStatus.Any(lv =>
                lv.Code.ToLower() == model.Code.ToLower()
                && (lv.Id != id)
                && !lv.DelDate.HasValue);

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
