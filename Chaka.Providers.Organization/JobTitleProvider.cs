using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.JobTitle;
using Chaka.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chaka.Utilities;

namespace Chaka.Providers.Organization
{
    public interface IJobTitleService : IDataService<JobTitle>
    {
        #region JobTitle
        IEnumerable<ListJobTitleViewModel> List();
        IEnumerable<JobStatus> GetJobStatus();
        IEnumerable<Level> GetLevel();
        IQueryable<JobFamily> GetJobFamily();
        DropDownListViewModel GetLevelCategory(int levelID);
        bool IsJobTitleCodeValid(string code, int id);
        AjaxViewModel ValidateCombination(CreateEditViewModel model);
        bool ValidateDelete(int jobTitleID);
        IndexDetailViewModel GetDetailIndex(int jobTitleID);
        #endregion JobTtitle

        #region JobTitleDescription
        IQueryable<JobDescription> GetDescription();
        JobDescription GetDescription(int Id);
        IEnumerable<ListDescriptionViewModel> GetDescriptionList(int jobTitleID);
        int[] GetJobTitleID(int JobTitleID);
        IEnumerable<ListDescriptionViewModel> ListSelected(int JobTitleID, int[] selectedID);
        IEnumerable<ListDescriptionViewModel> ListUnselected(int JobTitleID, int[] selectedID);
        void UpdateListJobTitle(int JobTitleID, string[] JobMasterID);
        #endregion JobTitleDescription

        #region JobSpesification
        IQueryable<ListSpesificationViewModel> GetSpesificationList(int jobTitleID);
        IEnumerable<ListDescriptionViewModel> ListMajorSelected(int JobTitleSpecificationID, int[] unselectedID);
        IEnumerable<ListDescriptionViewModel> ListMajorUnselected(int JobTitleSpecificationID, int[] selectedID);
        int[] GetJobTitleSpecificationID(int JobTitleSpecificationID);
        IQueryable<LineOfBusiness> GetLineOfBusiness();
        IQueryable<JobFunction> GetJobFunction();
        IQueryable<LevelEdu> GetLevelEdu();
        int AddSpecification(JobtitleSpecification entity);
        void UpdateListJobTitleSpecificationMajor(int JobTitleSpecificationID, string[] MajorID);
        int EditSpecification(JobtitleSpecification entity);
        void DeleteSpecification(int Id);
        JobtitleSpecification GetSpesification(int Id);
        bool SpecificationValidateCombination(JobtitleSpecification model);
        #endregion JobSpesification
    }

    public class JobTitleProvider : IJobTitleService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;
        int CurrentUserId = 2;

        public JobTitleProvider(ChakaContext context)
        {
            this.context = context;
        }

        #region Query

        public IQueryable<Level> AllLevel()
        {
            return context.Level.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<LevelCategory> AllLevelCategory()
        {
            return context.LevelCategory.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<EducationMajor> AllMajor()
        {
            return context.EducationMajor.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<JobDescription> AllJobTitleDescription()
        {
            return context.JobDescription.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<JobtitleSpecification> AllJobTitleSpesification()
        {
            return context.JobtitleSpecification.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<LineOfBusiness> AllLineOfBusiness()
        {
            return context.LineOfBusiness.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<JobFunction> AllJobFunctions()
        {
            return context.JobFunction.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<LevelEdu> AllLevelEdu()
        {
            return context.LevelEdu.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<JobMaster> AllJobMasters()
        {
            return context.JobMaster.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        public IQueryable<JobtitleSpecificationMajor> AlljobtitleSpecificationMajors()
        {
            return context.JobtitleSpecificationMajor.Where(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue);
        }

        #endregion Query

        #region Populate DropDown
        public IEnumerable<JobStatus> GetJobStatus()
        {
            var query = context.JobStatus.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }

        public IEnumerable<Level> GetLevel()
        {
            var query = context.Level.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }

        public IQueryable<JobFamily> GetJobFamily() => context.JobFamily.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);
        public DropDownListViewModel GetLevelCategory(int levelID)
        {
            var query = (from lv in context.Level
                         join cat in context.LevelCategory on lv.LevelCategoryId equals cat.Id
                         where lv.Id == levelID
                         select new DropDownListViewModel
                         {
                             Value = Convert.ToString(cat.Id),
                             Text = cat.Name
                         }).FirstOrDefault();
            if (query == null)
            {
                query.Value = "";
                query.Text = "";
            }
            return query;
        }

        public IEnumerable<EducationMajor> GetMajor()
        {
            var query = context.EducationMajor.Where(js => js.IsActive && !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }
        #endregion PopulateDropDown

        #region JobTitle
        public IQueryable<JobTitle> Get() => context.JobTitle.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);

        public JobTitle Get(int Id) => context.JobTitle.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);

        public IEnumerable<ListJobTitleViewModel> List()
        {
            var query = from jt in Get()
                        join jl in AllLevel() on jt.JobLevelId equals jl.Id
                        join jc in AllLevelCategory() on jt.JobLevelCategoryId equals jc.Id
                        select new ListJobTitleViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(jt.Id)),
                            Code = jt.Code,
                            Name = jt.Name,
                            Description = jt.Description == null ? "N/A" : jt.Description,
                            JobStatus = jt.JobStatus.Name,
                            JobLevel = jl.Name,
                            JobLevelCategory = jc.Name
                        };

            return query;
        }

        public int Add(JobTitle entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(JobTitle entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int id)
        {
            var jobTitle = Get(id);
            if (jobTitle != null)
            {
                context.SbDelete(jobTitle);
                context.SaveChanges();
            }
        }

        public bool IsJobTitleCodeValid(string code, int id)
        {
            var result = Get().Any(jt => jt.Code == code && jt.Id != id);
            return result;
        }

        public AjaxViewModel ValidateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedJobTitle = context.JobTitle.SingleOrDefault(jt =>
                jt.JobStatusId == model.JobStatusID
                && jt.JobLevelId == model.JobLevelID
                && jt.JobLevelCategoryId == model.JobLevelCategoryID
                && (jt.Id != id)
                && !jt.DelDate.HasValue
                && jt.BusinessGroupId == businessGroupID);

            var existedCode = context.JobTitle.Any(jt =>
                jt.Code.ToLower() == model.Code.ToLower()
                && (jt.Id != id)
                && !jt.DelDate.HasValue
                && jt.BusinessGroupId == businessGroupID);


            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Code has been used";
            }
            else if (existedJobTitle != null)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Combination data is not valid";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        public bool ValidateDelete(int id)
        {
            var usedByDescription = context.JobDescription.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.JobtitleId == id);
            var usedByRequirement = context.JobtitleRequirement.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.JobtitleId == id);
            //var usedByCandidateApplication = context.CandidateApplication.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ApplyFor == jobTitleID);
            //var usedByMedicalCheckup = context.MedicalCheckup.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ForJobTitleID == jobTitleID);
            //var usedByRecruitmentTest = context.RecruitmentTest.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ForJobTitleID == jobTitleID);
            //var usedByProjectActivityVacancy = context.ProjectActivityVacancy.Any(m => m.BusinessGroupID == CurrentBusinessGroupId
            //    && m.JobTitleID == jobTitleID);

            var used = usedByDescription || usedByRequirement;
            //var used = usedByCandidateApplication || usedByMedicalCheckup || usedByProjectActivityVacancy || usedByRecruitmentTest;
            return used;

        }
        #endregion JobTitle

        public IndexDetailViewModel GetDetailIndex(int jobTitleID)
        {
            var query = (from jt in Get()
                         join jl in AllLevel() on jt.JobLevelId equals jl.Id
                         join jc in AllLevelCategory() on jt.JobLevelCategoryId equals jc.Id
                         where jt.Id == jobTitleID
                         let beginEff = jt.BeginEff == null ? "N/A" : string.Format("{0:dd MMM yyyy}", jt.BeginEff)
                         let lastEff = jt.LastEff == null ? "N/A" : string.Format("{0:dd MMM yyyy}", jt.LastEff)
                         select new IndexDetailViewModel
                         {
                             ID = jt.Id,
                             JobTitleCode = jt.Code,
                             JobTitleName = jt.Name,
                             Description = jt.Description == null ? "N/A" : jt.Description,
                             JobStatus = jt.JobStatus.Name,
                             JobLevel = jl.Name,
                             JobLevelCategory = jc.Name,
                             BeginEff = beginEff,
                             LastEff = lastEff
                         }).SingleOrDefault();

            return query;
        }

        #region JobDescription
        public IQueryable<JobDescription> GetDescription() => context.JobDescription.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);

        public JobDescription GetDescription(int Id) => context.JobDescription.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);

        public IEnumerable<ListDescriptionViewModel> GetDescriptionList(int jobTitleID)
        {
            var query = from des in AllJobTitleDescription()
                        join jm in context.JobMaster on des.JobMasterId equals jm.Id into jmlefttable
                        from jmleft in jmlefttable.DefaultIfEmpty()
                        where des.JobtitleId == jobTitleID
                        select new ListDescriptionViewModel
                        {
                            ID = des.Id,
                            Code = jmleft.Code,
                            Name = jmleft.Name,
                            Description = jmleft.Description
                        };
            return query;
        }

        public int[] GetJobTitleID(int JobTitleID)
        {
            return AllJobTitleDescription().Where(e => e.JobtitleId == JobTitleID).Select(s => (int)s.JobMasterId).ToArray();
        }

        public IEnumerable<ListDescriptionViewModel> ListSelected(int JobTitleID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from jfm in AllJobMasters()
                       where !unselectedID.Contains(jfm.Id)
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
            else
            {
                return from jfm in AllJobMasters()
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
        }

        public IEnumerable<ListDescriptionViewModel> ListUnselected(int JobTitleID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from jfm in AllJobMasters()
                       where !selectedID.Contains(jfm.Id)
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
            else
            {
                return from jfm in AllJobMasters()
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
        }

        public void UpdateListJobTitle(int JobTitleID, string[] JobMasterID)
        {
            var filterJobTitleDescription = AllJobTitleDescription().Where(e => e.JobtitleId == JobTitleID);

            if (JobMasterID != null)
            {
                int[] arrayJobMasterID = JobMasterID.Select(g => Convert.ToInt32(g)).ToArray();

                if (filterJobTitleDescription != null)
                {
                    foreach (var jobfam in filterJobTitleDescription)
                    {
                        if (!arrayJobMasterID.Contains((int)jobfam.JobMasterId))
                            context.JobDescription.Remove(jobfam);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayJobMasterID)
                {
                    if (!filterJobTitleDescription.Any(g => g.JobMasterId == id))
                    {
                        context.JobDescription.Add(new JobDescription()
                        {
                            JobtitleId = JobTitleID,
                            JobMasterId = id,
                            BusinessGroupId = businessGroupID,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterJobTitleDescription != null)
                {
                    foreach (var jobfam in filterJobTitleDescription)
                    {
                        context.JobDescription.Remove(jobfam);
                    }
                }
            }

            context.SaveChanges();
        }


        #endregion JobDescription
        //==================================================================================================================
        #region JobSpesification
        public IQueryable<JobtitleSpecification> GetSpesification() => context.JobtitleSpecification.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);

        public JobtitleSpecification GetSpesification(int Id) => context.JobtitleSpecification.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);

        public IQueryable<ListSpesificationViewModel> GetSpesificationList(int jobTitleID)
        {
            var query = from req in AllJobTitleSpesification()
                        join lob in AllLineOfBusiness() on req.LineId equals lob.Id into ljlob
                        from leftlob in ljlob.DefaultIfEmpty()
                        join jb in AllJobFunctions() on req.JobFunctionId equals jb.Id into ljjb
                        from leftjb in ljjb.DefaultIfEmpty()
                        join le in AllLevelEdu() on req.LevelEduId equals le.Id into lejb
                        from leftle in lejb.DefaultIfEmpty()
                        where req.JobTitleId == jobTitleID
                        select new ListSpesificationViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(req.Id)),
                            LineOfBussiness = leftlob.Name,
                            JobFunction = leftjb.Name,
                            LevelEducation = leftle.Name,
                            MinExp = req.MinExp == null ? 0 : req.MinExp,
                            Descriptions = req.Descriptions == null ? "N/A" : req.Descriptions.ToString()
                        };

            return query;
        }

        public int[] GetJobTitleSpecificationID(int JobTitleSpecificationID)
        {
            return AlljobtitleSpecificationMajors().Where(e => e.JobtitleSpecificationId == JobTitleSpecificationID).Select(s => (int)s.MajorId).ToArray();
        }

        public IEnumerable<ListDescriptionViewModel> ListMajorSelected(int JobTitleSpecificationID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from jfm in AllMajor()
                       where !unselectedID.Contains(jfm.Id)
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
            else
            {
                return from jfm in AllJobMasters()
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
        }

        public IEnumerable<ListDescriptionViewModel> ListMajorUnselected(int JobTitleSpecificationID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from jfm in AllMajor()
                       where !selectedID.Contains(jfm.Id)
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
            else
            {
                return from jfm in AllMajor()
                       select new ListDescriptionViewModel()
                       {
                           ID = jfm.Id,
                           Code = jfm.Code,
                           Name = jfm.Name,
                           Description = jfm.Description
                       };
            }
        }

        public IQueryable<LineOfBusiness> GetLineOfBusiness() => context.LineOfBusiness.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<JobFunction> GetJobFunction() => context.JobFunction.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<LevelEdu> GetLevelEdu() => context.LevelEdu.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public int AddSpecification(JobtitleSpecification entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int EditSpecification(JobtitleSpecification entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void DeleteSpecification(int Id)
        {
            var entity = context.JobtitleSpecification.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);
            if (entity != null)
            {
                context.SbDelete(entity);
                context.SaveChanges();
            }
        }

        public void UpdateListJobTitleSpecificationMajor(int JobTitleSpecificationID, string[] MajorID)
        {
            var filter = AlljobtitleSpecificationMajors().Where(e => e.JobtitleSpecificationId == JobTitleSpecificationID);

            if (MajorID != null)
            {
                int[] arrayMajorID = MajorID.Select(g => Convert.ToInt32(g)).ToArray();

                if (filter != null)
                {
                    foreach (var dt in filter)
                    {
                        if (!arrayMajorID.Contains((int)dt.JobtitleSpecificationId))
                            context.JobtitleSpecificationMajor.Remove(dt);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayMajorID)
                {
                    if (!filter.Any(g => g.MajorId == id))
                    {
                        context.JobtitleSpecificationMajor.Add(new JobtitleSpecificationMajor()
                        {
                            JobtitleSpecificationId = JobTitleSpecificationID,
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
                if (filter != null)
                {
                    foreach (var dt in filter)
                    {
                        context.JobtitleSpecificationMajor.Remove(dt);
                    }
                }
            }

            context.SaveChanges();
        }

        public bool SpecificationValidateCombination(JobtitleSpecification model)
        {
            var id = model.Id;
            bool check;
            if(id < 1)
            {
                check = context.JobtitleSpecification.Any(jt =>
                    jt.JobTitleId == model.JobTitleId
                    && jt.LineId == model.LineId
                    && jt.JobFunctionId == model.JobFunctionId
                    && jt.LevelEduId == model.LevelEduId
                    && !jt.DelDate.HasValue
                    && jt.BusinessGroupId == businessGroupID);
            }
            else
            {
                check = context.JobtitleSpecification.Any(jt =>
                    jt.JobTitleId == model.JobTitleId
                    && jt.LineId == model.LineId
                    && jt.JobFunctionId == model.JobFunctionId
                    && jt.LevelEduId == model.LevelEduId
                    && (jt.Id != id)
                    && !jt.DelDate.HasValue
                    && jt.BusinessGroupId == businessGroupID);
            }
            return check;
        }
        #endregion JobSpesification


    }
}
