using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.Country;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ICountryService : IDataService<Country>
    {
        IQueryable<ListViewModel> GetList();
        bool IsCodeValid(string code, int id);
        AjaxViewModel ValidateCombination(CreateEditViewModel model);
        
    }
    public class CountryProvider : ICountryService
    {
        ChakaContext context;
        int businessGroupID = 2;
        public CountryProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Country> AllCountries
        {
            get { return context.Country.AsNoTracking().Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Province> AllProvinces
        {
            get { return context.Province.AsNoTracking().Where(x => !x.DelDate.HasValue); }
        }
        public IQueryable<Country> Get()
        {
            return AllCountries;
        }
        public Country Get(int Id)
        {
            return AllCountries.SingleOrDefault(x => x.Id == Id);
        }
        public int Add(Country entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var country = Get(Id);
            if(country != null)
            {
                context.SbDelete(country);
                context.SaveChanges();
            }
        }

        public int Edit(Country entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IQueryable<ListViewModel> GetList()
        {
            var query = from country in AllCountries
                        select new ListViewModel()
                        {
                            ID = country.Id.ToString(),
                            Code = country.Code,
                            Name = country.Name,
                            Description = country.Description
                        };
            return query;
        }


        public bool IsCodeValid(string code, int id)
        {
            var result = AllCountries.Any(x => x.Code == code && x.Id != id);
            return result;
        }

        public AjaxViewModel ValidateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.Country.Any(lv =>
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




    }
}
