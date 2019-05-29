using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.GroupGrade;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Chaka.Providers.Organization
{
    public interface IGroupGradeService : IDataService<GradeGroup>
    {
        IQueryable<ListGroupGradeViewModel> GetList();
        IQueryable<ListGradeDetailViewModel> ListGradeDetail(int groupGradeId);
        AjaxViewModel validateCombination(CreateEditViewModel model);
        //AjaxViewModel validateCombinationGrade(CreateEditGradeDetailViewModel model, int groupGradeID);
        AjaxViewModel validateCombinationGrade(CreateEditGradeDetailViewModel model);

        bool IsGroupGradeCodeValid(string code, string id);
        bool IsGradeDetailCodeValid(string code, string id);
        bool IsGradeDetailNameValid(string name, string id);
        Grade GetGradeDetail(int Id);
        int AddGradeDetail(Grade entity);
        int EditGradeDetail(Grade entity);
        void DeleteGradeDetail(int Id);
        AjaxViewModel IsDeleteGroupGradeValid(int id);
        int MaxSequenceByGroupGradeId(int groupGradeId);
        //void UpdateSequenceLevel(int sequenceBefore, int sequenceNow, int groupGradeID);
        bool IsUsedByEmployee(int GradeID);
    }
    public class GroupGradeProvider : IGroupGradeService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;
        public GroupGradeProvider(ChakaContext context)
        {
            this.context = context;
        }

        #region GroupGrade

        public IQueryable<GradeGroup> AllGroupGrades
        {
            get { return context.GradeGroup.Where(x => !x.DelDate.HasValue); }
        }
        public IQueryable<GradeGroup> Get() => context.GradeGroup.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public GradeGroup Get(int Id) => context.GradeGroup.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListGroupGradeViewModel> GetList()
        {
            var CurrentBusinessGrupID = TokenDataClaim.CurrentBusinessGrupID;
            var LoginName = TokenDataClaim.LoginName;
            var CurrentEmail = TokenDataClaim.CurrentEmail;
            var Password = TokenDataClaim.Password;
            var query = from data in AllGroupGrades
                        select new ListGroupGradeViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            Description = data.Description
                        };
            return query;
        }

        public int Add(GradeGroup entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(GradeGroup entity)
        {
            context.SbEdit(entity);
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



        public bool IsGroupGradeCodeValid(string code, string id)
        {
            var result = AllGroupGrades.Any(GG => GG.Code == code && GG.Id != Convert.ToInt32(id));
            return result;
        }

        public AjaxViewModel IsDeleteGroupGradeValid(int id)
        {
            var viewModel = new AjaxViewModel();
            var result = context.Grade.Any(grade => grade.GroupGradeId == id);
            if (!result)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Enable Delete";
            }
            viewModel.IsSuccess = true;

            return viewModel;
        }

        public bool IsUsedByEmployee(int GradeID)
        {
            var result = context.Employee.Any(e => e.GradeId == GradeID);

            return result;
        }
        #endregion

        #region Grades
        public IQueryable<Grade> AllGrades
        {
            get { return context.Grade.Where(x => !x.DelDate.HasValue); }
        }

        public bool IsGradeDetailCodeValid(string code, string id)
        {
            var result = AllGrades.Any(G => G.Code == code && G.Id != Convert.ToInt32(id));
            return result;
        }

        public bool IsGradeDetailNameValid(string name, string id)
        {
            var result = AllGrades.Any(G => G.Name == name && G.Id != Convert.ToInt32(id));
            return result;
        }

        public Grade GetGradeDetail(int Id) => context.Grade.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public Grade Get(int groupGradeID, int Id) => context.Grade.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id && x.GroupGradeId == groupGradeID);

        public IQueryable<ListGradeDetailViewModel> ListGradeDetail(int groupGradeId)
        {
            var query = from data in AllGrades
                        where data.GroupGradeId == groupGradeId
                        select new ListGradeDetailViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            Description = data.Description,
                            Sequence = data.Sequence
                        };
            return query;
        }

        public int AddGradeDetail(Grade entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int EditGradeDetail(Grade entity)
        {

            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void DeleteGradeDetail(int Id)
        {
            var menu = GetGradeDetail(Id);
            if (menu != null)
            {
                menu.Sequence = 0;
                context.SbDelete(menu);
                context.SaveChanges();
            }
        }

        public int MaxSequenceByGroupGradeId(int groupGradeId)
        {
            int maxSequence = 0;
            var data = context.Grade.Where(x => !x.DelDate.HasValue && x.GroupGradeId.Equals(groupGradeId)).OrderByDescending(x => x.Sequence).FirstOrDefault();
            if (data != null)
            {
                maxSequence = (int)data.Sequence;
            }
            return maxSequence;
        }
        #endregion

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.GradeGroup.Any(lv =>
                lv.Code.ToLower() == model.Code.ToLower()
                && (lv.Id != id)
                && !lv.DelDate.HasValue
                && lv.BusinessGroupId == businessGroupID);

            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = " Group Grade Code has been used ";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        public AjaxViewModel validateCombinationGrade(CreateEditGradeDetailViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.Grade.Any(gr =>
                gr.Code.ToLower() == model.Code.ToLower()
                && (gr.Id != id)
                //&&(gr.GroupGradeId == groupGradeID)
                && !gr.DelDate.HasValue
                && gr.BusinessGroupId == businessGroupID);

            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = " Grade Code has been used ";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        #region Sequence

        //public void UpdateSequenceLevel(int sequenceBefore, int sequenceNow, int groupGradeID)
        //{
        //    var selectedGrade = AllGrades.SingleOrDefault(grade => grade.Sequence == sequenceNow && grade.GroupGradeId == groupGradeID);
        //    if (selectedGrade != null)
        //    {
        //        selectedGrade.Sequence = sequenceBefore;
        //        if (selectedGrade.Id == 0)
        //        {
        //            context.SbAdd(selectedGrade);
        //        }
        //        else
        //        {
        //            context.SbEdit(selectedGrade);
        //        }
        //    }
        //}

        //public void UpdateSequenceLevelAfterDelete(int Sequence)
        //{
        //    IEnumerable<Grade> grades = AllGrades.Where(grade => grade.Sequence > Sequence);
        //    if (grades != null)
        //    {
        //        foreach (var grade in grades)
        //        {
        //            UpdateSequenceLevelByID(grade.Id);
        //        }
        //    }
        //}

        //public void UpdateSequenceLevelByID(int gradeID)
        //{
        //    var selectedGrade = AllGrades.SingleOrDefault(grade => grade.Id == gradeID);
        //    if (selectedGrade != null)
        //    {
        //        selectedGrade.Sequence = selectedGrade.Sequence - 1;
        //        context.SbEdit(selectedGrade);
        //    }
        //}


        #endregion

    }
}
