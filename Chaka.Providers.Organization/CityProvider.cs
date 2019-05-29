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

    public interface ICityService : IDataService<City>
    {
        IQueryable<ListCityViewModel> GetList(int provinceID);
        AjaxViewModel ValidateCombination(CreateEditCityViewModel model);
        bool IsCodeValid(string code, int id);
    }
    public class CityProvider : ICityService
    {
        ChakaContext context;
        int businessGroupID = 2;
        public CityProvider(ChakaContext context)
        {
            this.context = context;
        }
        public IQueryable<City> AllCities
        {
            get { return context.City.AsNoTracking().Where(x => !x.DelDate.HasValue); }
        }
        public int Add(City entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var city = Get(Id);
            if(city != null)
            {
                context.SbDelete(city);
                context.SaveChanges();
            }
        }

        public int Edit(City entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IQueryable<City> Get()
        {
            return AllCities;
        }

        public City Get(int id)
        {
            return AllCities.SingleOrDefault(x => x.Id == id);
        }

        public IQueryable<ListCityViewModel> GetList(int provinceID)
        {
            var query = from city in AllCities
                        where city.ProvinceId == provinceID
                        select new ListCityViewModel()
                        {
                            ID = city.Id.ToString(),
                            Code = city.Code,
                            Name = city.Name,
                            Description = city.Description
                        };
            return query;

        }

        public bool IsCodeValid(string code, int id)
        {
            var result = AllCities.Any(x => x.Code == code && x.Id != id);
            return result;
        }

        public AjaxViewModel ValidateCombination(CreateEditCityViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.City.Any(lv =>
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
