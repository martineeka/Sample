using Chaka.Database.Context.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{

    public interface IMenuDetailService : IDataService<MenuDetail>
    {

    }
    public class MenuDetailProvider : IDataService<MenuDetail>
    {
        ChakaContext context;
        public MenuDetailProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<MenuDetail> AllMenUDetails
        {
            get { return context.MenuDetail.Where(x => !x.DelDate.HasValue); }
        }

        public int Add(MenuDetail entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var menuDetail = Get(Id);
            if(menuDetail != null)
            {
                context.SbDelete(menuDetail);
                context.SaveChanges();
            }
        }

        public int Edit(MenuDetail entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IQueryable<MenuDetail> Get()
        {
            throw new NotImplementedException();
        }

        public MenuDetail Get(int id)
        {
            return context.MenuDetail.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == id);
        }
    }

}
