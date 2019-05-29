using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.JobFamily;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IJobFamilyService : IDataService<JobFamily>
    {
        IQueryable<ListJobFamilyViewModel> GetList();
        bool IsJobFamilyCodeValid(string code, int id);
        bool IsJobFamilyCodeExist(string code);
        AjaxViewModel validateCombination(CreateEditViewModel model);
        int[] GetJobFamilyID(int JobFamilyID);
        IEnumerable<ListDetailViewModel> ListSelected(int JobFamilyID, int[] selectedID);
        IEnumerable<ListDetailViewModel> ListUnselected(int JobFamilyID, int[] selectedID);
        void UpdateListJobFamily(int JobFamilyID, string[] majorID);
        AjaxViewModel ValidateDelete(int id);
        IQueryable<ListDetailViewModel> GetMajorList(int JobFamilyID);
    }
    public class JobFamilyProvider : IJobFamilyService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;
        int CurrentUserId = 2;

        public JobFamilyProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<JobFamily> AllJobFamilies
        {
            get { return context.JobFamily.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<JobFamilyMajor> AllJobFamiliMajors
        {
            get { return context.JobFamilyMajor.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<EducationMajor> AllEducationMajors
        {
            get { return context.EducationMajor.Where(x => !x.DelDate.HasValue); }
        }


        public IQueryable<JobFamily> Get() => context.JobFamily.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public JobFamily Get(int Id) => context.JobFamily.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);
        
        public IQueryable<ListJobFamilyViewModel> GetList()
        {
            var query = from level in AllJobFamilies
                        select new ListJobFamilyViewModel()
                        {
                            ID = level.Id.ToString(),
                            Code = level.Code,
                            Name = level.Name,
                            Description = level.Description
                        };
            return query;
        }
        public int Add(JobFamily entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(JobFamily entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = Get(Id);
            if (entity != null)
            {
                context.SbDelete(entity);
                context.SaveChanges();
            }
        }

        public AjaxViewModel ValidateDelete(int id)
        {
            var JsonViewModel = new AjaxViewModel();
            var result = context.JobTitle.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.JobFamilyId == id);
            JsonViewModel.IsSuccess = result;
            JsonViewModel.Message = "JobFamily";
            return JsonViewModel;
        }


        public bool IsJobFamilyCodeValid(string code, int id = 0)
        {
            var result = AllJobFamilies.Any(jf => jf.Code == code && jf.Id != id);
            return result;
        }

        public bool IsJobFamilyCodeExist(string code)
        {
            var result = AllJobFamilies.Any(jf => jf.Code == code && jf.BusinessGroupId == 2);
            return result;
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.JobFamily.Any(lv =>
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



        public int[] GetJobFamilyID(int JobFamilyID)
        {
            return AllJobFamiliMajors.Where(e => e.JobFamilyId == JobFamilyID).Select(s => (int)s.MajorId).ToArray();
        }

        public IQueryable<ListDetailViewModel> GetMajorList(int JobFamilyID)
        {
            var query = from des in AllJobFamiliMajors
                        join jm in context.EducationMajor on des.MajorId equals jm.Id into jmlefttable
                        from jmleft in jmlefttable.DefaultIfEmpty()
                        where des.JobFamilyId == JobFamilyID
                        select new ListDetailViewModel
                        {
                            ID = des.Id,
                            Code = jmleft.Code,
                            Name = jmleft.Name
                        };
            return query;
        }

        public IEnumerable<ListDetailViewModel> ListSelected(int JobFamilyID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from jfm in AllEducationMajors
                       where !unselectedID.Contains(jfm.Id)
                       select new ListDetailViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name
                       };
            }
            else
            {
                return from jfm in AllEducationMajors
                       select new ListDetailViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name
                       };
            }
        }

        public IEnumerable<ListDetailViewModel> ListUnselected(int JobFamilyID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from jfm in AllEducationMajors
                       where !selectedID.Contains(jfm.Id)
                       select new ListDetailViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name
                       };
            }
            else
            {
                return from jfm in AllEducationMajors
                       select new ListDetailViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name
                       };
            }
        }

        public void UpdateListJobFamily(int JobFamilyID, string[] majorID)
        {
            var filterJobFamiliMajors = AllJobFamiliMajors.Where(e => e.JobFamilyId == JobFamilyID);

            if (majorID != null)
            {
                int[] arrayMajorID = majorID.Select(g => Convert.ToInt32(g)).ToArray();

                if (filterJobFamiliMajors != null)
                {
                    foreach (var jobfam in filterJobFamiliMajors)
                    {
                        if (!arrayMajorID.Contains((int)jobfam.MajorId))
                            context.JobFamilyMajor.Remove(jobfam);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayMajorID)
                {
                    if (!filterJobFamiliMajors.Any(g => g.MajorId == id))
                    {
                        context.JobFamilyMajor.Add(new JobFamilyMajor()
                        {
                            JobFamilyId = JobFamilyID,
                            MajorId = id,
                            BusinessGroupId = businessGroupID,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterJobFamiliMajors != null)
                {
                    foreach (var jobfam in filterJobFamiliMajors)
                    {
                        context.JobFamilyMajor.Remove(jobfam);
                    }
                }
            }

            context.SaveChanges();
        }
    }
}
