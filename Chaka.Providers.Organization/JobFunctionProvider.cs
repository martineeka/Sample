using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.JobFunction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IJobFunctionServices : IDataService<JobFunction>
    {
        IQueryable<ListJobFunctionViewModel> GetList();
        bool IsCodeValid(string Code, int Id);
        bool IsCodeExist(string Code);
    }
    public class JobFunctionProvider : IJobFunctionServices
    {
        ChakaContext _context;

        public JobFunctionProvider(ChakaContext context)
        {
            _context = context;
        }

        public IQueryable<JobFunction> AllJobFunctions()
        {
            return _context.JobFunction.Where(x => !x.DelDate.HasValue);
        }

        public IQueryable<JobFunction> Get() => AllJobFunctions().AsNoTracking();

        public JobFunction Get(int Id) => AllJobFunctions().AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public IQueryable<ListJobFunctionViewModel> GetList()
        {
            var query = from jf in AllJobFunctions()
                        select new ListJobFunctionViewModel
                        {
                            ID = jf.Id.ToString(),
                            Code = jf.Code,
                            Name = jf.Name,
                            Description = jf.Description
                        };
            return query;
        }

        public int Add(JobFunction entity)
        {
            _context.SbAdd(entity);
            return _context.SaveChanges();
        }

        public int Edit(JobFunction entity)
        {
            _context.SbEdit(entity);
            return _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = Get(Id);
            if (entity != null)
            {
                _context.SbDelete(entity);
                _context.SaveChanges();
            }
        }

        public bool IsCodeValid(string Code, int Id)
        {
            var result = AllJobFunctions().Any(x => x.Code == Code && x.Id != Id);
            return result;
        }

        public bool IsCodeExist(string Code)
        {
            var result = AllJobFunctions().Any(x => x.Code == Code);
            return result;
        }
    }
}
