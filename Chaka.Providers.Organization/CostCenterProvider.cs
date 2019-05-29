using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.CostCenter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ICostCenterService : IDataService<CostCenter>
    {
        IQueryable<ListCostCenterViewModel> GetList();
        AjaxViewModel validateCombination(CreateEditViewModel model);
        bool IsUsedByOrgUnit(int ID);
    }

    public class CostCenterProvider : ICostCenterService
    {
        ChakaContext context;

        public CostCenterProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<CostCenter> AllCostCenter
        {
            get { return context.CostCenter.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<CostCenter> Get() => AllCostCenter.AsNoTracking();

        public CostCenter Get(int id) => AllCostCenter.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public IQueryable<ListCostCenterViewModel> GetList()
        {
            var query = from data in AllCostCenter
                        select new ListCostCenterViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            Description = data.Description
                        };
            return query;
        }

        public int Add(CostCenter entity)
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

        public int Edit(CostCenter entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.CostCenter.Any(lv =>
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

        public bool IsUsedByOrgUnit(int ID)
        {
            var result = context.OrgUnit.Any(e => e.CostCenterId == ID);

            return result;
        }
    }
}
