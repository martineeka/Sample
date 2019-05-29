using Chaka.Database.Context.Models;
using Chaka.ViewModels.SystemAdmin.Menu;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chaka.Providers.SystemAdmin
{
    public interface IMenuService : IDataService<Menu>
    {
        IQueryable<ListMenuViewModel> GetList();
        IQueryable<ListMenuFunctionViewModel> GetListMenuFunction(int menuID);
        MenuDetailViewModel GetMenuDetailViewModel(int id);


        bool IsMenuNameInaValid(string name, int id);
        bool IsMenuNameEngValid(string name, int id);
        IEnumerable<ListMenuFunctionViewModel> ListSelected(int MenuID, int[] unselectedID);
        IEnumerable<ListMenuFunctionViewModel> ListUnselected(int MenuID, int[] selectedID);
        int[] GetMenuID(int MenuID);

        void UpdateListFunction(int MenuID, string[] functionID);
    }
    public class MenuProvider : IMenuService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;
        int CurrentUserId = 2;

        public MenuProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Menu> AllMenus
        {
            get { return context.Menu.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<MenuDetail> AllMenuDetails
        {
            get { return context.MenuDetail.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Function> AllFunctions
        {
            get { return context.Function.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Menu> Get() => context.Menu.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public Menu Get(int Id) => context.Menu.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListMenuViewModel> GetList()
        {
            var query = from data in AllMenus
                        select new ListMenuViewModel()
                        {
                            ID = data.Id.ToString(),
                            NameINA = data.NameIna,
                            NameENG = data.NameEng,
                            Description = data.Description
                        };
            return query;
        }

        public IQueryable<ListMenuFunctionViewModel> GetListMenuFunction(int menuID)
        {
            var query = from menuDetail in AllMenuDetails
                        where menuDetail.MenuId == menuID
                        select new ListMenuFunctionViewModel()
                        {
                            ID = menuDetail.Id,
                            DescriptionENG = menuDetail.Function.DescriptionEng,
                            DescriptionINA = menuDetail.Function.DescriptionIna,
                        };
          
            return query;
        }


        public int Add(Menu entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(Menu entity)
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

        public MenuDetailViewModel GetMenuDetailViewModel(int menuID)
        {
            var model = new MenuDetailViewModel();
            var menu = Get(menuID);
            model.Menu = menu;
            model.MenuID = menu.Id;
            model.Functions = GetFunctionAccessibilities(menuID);
            return model;
        }
        public IDictionary<Function, bool> GetFunctionAccessibilities(int menuID)
        {
            var result = new Dictionary<Function, bool>();
            var selectedMenu = Get(menuID);
            if (selectedMenu != null)
            {
                var functions = context.Function.Where(function => !function.DelDate.HasValue && !function.FunctionView.Contains("~/general/")).OrderBy(function => function.Name);
                foreach (var function in functions)
                {
                    bool any = false;
                    any = selectedMenu.MenuDetail.Any(menu => menu.FunctionId == function.Id && !menu.DelDate.HasValue);
                    result.Add(function, any);
                }
            }
            return result;
        }

        public bool IsMenuNameInaValid(string name, int id)
        {
            var result = AllMenus.Any(menu => menu.NameIna == name && menu.Id != id);
            return result;
        }

        public bool IsMenuNameEngValid(string name, int id)
        {
            var result = AllMenus.Any(menu => menu.NameEng == name && menu.Id != id);
            return result;
        }

        public IEnumerable<ListMenuFunctionViewModel> ListSelected(int MenuID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from lmf in AllFunctions
                       where !unselectedID.Contains(lmf.Id)
                       select new ListMenuFunctionViewModel()
                       {
                           ID = lmf.Id,
                           DescriptionENG = lmf.DescriptionEng,
                           DescriptionINA = lmf.DescriptionIna
                       };
            }
            else
            {
                return from lmf in AllFunctions
                       select new ListMenuFunctionViewModel()
                       {
                           ID = lmf.Id,
                           DescriptionENG = lmf.DescriptionEng,
                           DescriptionINA = lmf.DescriptionIna
                       };
            }
        }

        public IEnumerable<ListMenuFunctionViewModel> ListUnselected(int MenuID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from lmf in AllFunctions
                       where !selectedID.Contains(lmf.Id)
                       select new ListMenuFunctionViewModel()
                       {
                           ID = lmf.Id,
                           DescriptionENG = lmf.DescriptionEng,
                           DescriptionINA = lmf.DescriptionIna
                       };
            }
            else
            {
                return from lmf in AllFunctions
                       select new ListMenuFunctionViewModel()
                       {
                           ID = lmf.Id,
                           DescriptionENG = lmf.DescriptionEng,
                           DescriptionINA = lmf.DescriptionIna
                       };
            }
        }

        public int[] GetMenuID(int MenuID)
        {
            return AllMenuDetails.Where(e => e.MenuId == MenuID).Select(s => (int)s.FunctionId).ToArray();
        }

        public void UpdateListFunction(int MenuID, string[] functionID)
        {
            var filterMenuDetails = AllMenuDetails.Where(e => e.MenuId == MenuID);

            if (functionID != null)
            {
                int[] arrayFunctionID = functionID.Select(g => Convert.ToInt32(g)).ToArray();

                if (filterMenuDetails != null)
                {
                    foreach (var menuFunc in filterMenuDetails)
                    {
                        if (!arrayFunctionID.Contains((int)menuFunc.FunctionId))
                            context.MenuDetail.Remove(menuFunc);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayFunctionID)
                {
                    if (!filterMenuDetails.Any(g => g.FunctionId == id))
                    {
                        context.MenuDetail.Add(new MenuDetail()
                        {
                            MenuId = MenuID,
                            FunctionId = id,
                            BusinessGroupId = businessGroupID,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterMenuDetails != null)
                {
                    foreach (var menuFunc in filterMenuDetails)
                    {
                        context.MenuDetail.Remove(menuFunc);
                    }
                }
            }

            context.SaveChanges();
        }


    }
}
