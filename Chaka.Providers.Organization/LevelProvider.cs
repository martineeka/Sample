using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.Level;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ILevelService  : IDataService<Level>
    {
        IQueryable<ListLevelViewModel> GetList();
        int GetMaxSequenceLevel();
        bool IsLevelCodeValid(string code, int decryptID);
        AjaxViewModel validateCombination(CreateEditViewModel model);
        //void UpdateSequenceLevel(int sequenceLevelBefore, int sequenceLevelNow);
    }

    public class LevelProvider : ILevelService
    {
        readonly ChakaContext context;


        public LevelProvider(ChakaContext context)
        {
            this.context = context;
        }


        public IQueryable<Level> AllLevels
        {
            get { return context.Level.Where(x => !x.DelDate.HasValue); }
        }
        public IQueryable<Level> Get() => context.Level.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public Level Get(int Id) => context.Level.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListLevelViewModel> GetList()
        {
            var query = from level in AllLevels
                        join levelCat in context.LevelCategory on level.LevelCategoryId equals levelCat.Id
                        select new ListLevelViewModel()
                        {
                            ID = level.Id.ToString(),
                            Code = level.Code,
                            Name = level.Name,
                            Description = level.Description,
                            Sequence = level.Sequence,
                            LevelCategory = levelCat.Name
                        };
            return query;
        }
        public int Add(Level entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(Level entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var level = Get(Id);
            if (level != null)
            {
                context.SbDelete(level);
                context.SaveChanges();
            }
        }

        public int GetMaxSequenceLevel()
        {
            int maxSequenceLevel = 0;
            var sequence = AllLevels.OrderByDescending(level => level.Sequence).FirstOrDefault();
            if (sequence != null)
                maxSequenceLevel = sequence.Sequence;
            return maxSequenceLevel;
        }

        public bool IsLevelCodeValid(string code, int id)
        {
            var result = AllLevels.Any(level => level.Code == code && level.Id != id);
            return result;
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.Level.Any(lv =>
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
