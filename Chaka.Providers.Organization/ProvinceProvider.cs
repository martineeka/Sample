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
    public interface IProvinceService : IDataService<Province>
    {
        AjaxViewModel ValidateCombination(CreateEditProvinceViewModel model);
        IQueryable<ListProvinceViewModel> GetList();
        IQueryable<ListProvinceViewModel> GetListByCountryID(int countryID);
        bool IsCodeValid(string code, int id);
        Province GetIndexCity(int provinceID);
    }
    public class ProvinceProvider : IProvinceService
    {
        ChakaContext context;
        int businessGroupID = 2;
        public ProvinceProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Province> AllProvinces
        {
            get { return context.Province.AsNoTracking().Where(x => !x.DelDate.HasValue); }
        }
        public IQueryable<Province> Get()
        {
            return AllProvinces;
        }
        public Province Get(int Id)
        {
            return AllProvinces.SingleOrDefault(x => x.Id == Id);
        }
        public Province GetIndexCity(int provinceID)
        {
            return AllProvinces.SingleOrDefault(x => x.Id == provinceID);
        }

        public int Add(Province entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var province = Get(Id);
            if (province != null)
            {
                context.SbDelete(province);
                context.SaveChanges();
            }
        }

        public int Edit(Province entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IQueryable<ListProvinceViewModel> GetList()
        {
            var query = from province in AllProvinces
                        select new ListProvinceViewModel()
                        {
                            ID = province.Id.ToString(),
                            Code = province.Code,
                            Name = province.Name,
                            Description = province.Description
                        };
            return query;
        }

        public IQueryable<ListProvinceViewModel> GetListByCountryID(int countryID)
        {
            var query = from province in AllProvinces.Where(x => x.CountryId == countryID)
                        select new ListProvinceViewModel()
                        {
                            ID = province.Id.ToString(),
                            Code = province.Code,
                            Name = province.Name,
                            Description = province.Description
                        };
            return query;
        }

        public bool IsCodeValid(string code, int id)
        {
            var result = AllProvinces.Any(x => x.Code == code && x.Id != id);
            return result;
        }

        public AjaxViewModel ValidateCombination(CreateEditProvinceViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.Province.Any(lv =>
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
