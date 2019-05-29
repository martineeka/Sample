using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.SIUPClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ISIUPClassService : IDataService<Siupclass>
    {
        IQueryable<ListSIUPClassViewModel> GetList();
        AjaxViewModel validateCombination(CreateEditViewModel model);
        bool IsUsedByLegalEntity(int ID);
    }

    public class SIUPClassProvider : ISIUPClassService
    {
        ChakaContext context;

        public SIUPClassProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Siupclass> AllSIUPClass
        {
            get { return context.Siupclass.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Siupclass> Get() => AllSIUPClass.AsNoTracking();

        public Siupclass Get(int id) => AllSIUPClass.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public IQueryable<ListSIUPClassViewModel> GetList()
        {
            var query = from data in AllSIUPClass
                        select new ListSIUPClassViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            AssetFrom = data.AssetFrom,
                            AssetTo = data.AssetTo
                        };
            return query;
        }

        public int Add(Siupclass entity)
        {
            context.SbAdd(entity);
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

        public int Edit(Siupclass entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.Siupclass.Any(lv =>
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

        public bool IsUsedByLegalEntity(int ID)
        {
            var result = context.LegalEntityInformation.Any(e => e.SiupclassId == ID);

            return result;
        }
    }
}
