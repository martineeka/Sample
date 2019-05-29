using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.LineOfBusiness;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ILineOfBusinessServices : IDataService<LineOfBusiness>
    {
        IQueryable<ListLineOfBusinessViewModel> GetList();
        bool IsCodeValid(string Code, int Id);
        bool IsCodeExist(string Code);
    }
    public class LineOfBusinessProvider : ILineOfBusinessServices
    {
        ChakaContext _context;

        public LineOfBusinessProvider(ChakaContext context)
        {
            _context = context;
        }

        public IQueryable<LineOfBusiness> AllLineOfBusiness()
        {
            return _context.LineOfBusiness.Where(x => !x.DelDate.HasValue);
        }

        public IQueryable<LineOfBusiness> Get() => AllLineOfBusiness().AsNoTracking();

        public LineOfBusiness Get(int Id) => AllLineOfBusiness().AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public IQueryable<ListLineOfBusinessViewModel> GetList()
        {
            var query = from lob in AllLineOfBusiness()
                        select new ListLineOfBusinessViewModel
                        {
                            ID = lob.Id.ToString(),
                            Code = lob.Code,
                            Name = lob.Name,
                            Description = lob.Description
                        };
            return query;
        }

        public int Add(LineOfBusiness entity)
        {
            _context.SbAdd(entity);
            return _context.SaveChanges();
        }

        public int Edit(LineOfBusiness entity)
        {
            _context.SbEdit(entity);
            return _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = Get(Id);
            if(entity != null)
            {
                _context.SbDelete(entity);
                _context.SaveChanges();
            }
        }

        public bool IsCodeValid(string Code, int Id)
        {
            var result = AllLineOfBusiness().Any(x => x.Code == Code && x.Id != Id);
            return result;
        }

        public bool IsCodeExist(string Code)
        {
            var result = AllLineOfBusiness().Any(x => x.Code == Code);
            return result;
        }

    }
}
