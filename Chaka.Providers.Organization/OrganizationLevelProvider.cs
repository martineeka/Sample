using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.OrganizationLevel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IOrganizationLevelService : IDataService<OrganizationLevel>
    {
        IQueryable<ListOrganizationLevelViewModel> GetList();
        AjaxViewModel validateCombination(CreateEditViewModel model);
        bool IsUsedByOrgUnit(int ID);
    }

    public class OrganizationLevelProvider : IOrganizationLevelService
    {
        ChakaContext context;

        public OrganizationLevelProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<OrganizationLevel> AllOrganizationLevel
        {
            get { return context.OrganizationLevel.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<OrganizationLevel> Get() => AllOrganizationLevel.AsNoTracking();

        public OrganizationLevel Get(int id) => AllOrganizationLevel.AsNoTracking().FirstOrDefault(x => x.Id == id);

        public IQueryable<ListOrganizationLevelViewModel> GetList()
        {
            var query = from data in AllOrganizationLevel
                        select new ListOrganizationLevelViewModel()
                        {
                            ID = data.Id.ToString(),
                            Code = data.Code,
                            Name = data.Name,
                            Description = data.Description,
                            Level = data.Level
                        };
            return query;
        }

        public int Add(OrganizationLevel entity)
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

        public int Edit(OrganizationLevel entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.OrganizationLevel.Any(lv =>
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
            var result = context.OrgUnit.Any(e => e.OrganizationlevelId == ID);

            return result;
        }
    }
}
