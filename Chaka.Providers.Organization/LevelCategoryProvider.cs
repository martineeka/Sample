using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.LevelCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{

    public interface ILevelCategoryService : IDataService<LevelCategory>
    {
        IQueryable<ListLevelCategoryViewModel> GetList();
        int GetMaxSequenceLevel();
        bool IsLevelCategoryCodeValid(string code, int id);
        bool IsLevelCategoryCodeValidNotId(string code);
    }
    public class LevelCategoryProvider : ILevelCategoryService
    {
        readonly ChakaContext context;

        public LevelCategoryProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<LevelCategory> AllLevelCategories
        {
            get { return context.LevelCategory.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<LevelCategory> Get() => context.LevelCategory.AsNoTracking().Where(x => !x.DelDate.HasValue);
        public LevelCategory Get(int Id) => context.LevelCategory.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListLevelCategoryViewModel> GetList()
        {
            var query = from level in AllLevelCategories
                        select new ListLevelCategoryViewModel()
                        {
                            ID = level.Id.ToString(),
                            Code = level.Code,
                            Name = level.Name,
                            Description = level.Description,
                            Sequence = (int)level.Sequence
                        };
            return query;
        }

        public int Add(LevelCategory entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(LevelCategory entity)
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
            var sequence = AllLevelCategories.OrderByDescending(level => level.Sequence).FirstOrDefault();
            if (sequence != null)
                maxSequenceLevel = (int)sequence.Sequence;
            return maxSequenceLevel;
        }

        public bool IsLevelCategoryCodeValid(string code, int id)
        {
            var result = AllLevelCategories.Any(level => level.Code == code && level.Id != id);
            return result;
        }

        public bool IsLevelCategoryCodeValidNotId(string code)
        {
            var result = AllLevelCategories.Any(level => level.Code == code);
            return result;
        }
    }
}
