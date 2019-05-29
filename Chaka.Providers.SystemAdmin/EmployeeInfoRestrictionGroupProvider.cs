using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.SystemAdmin.EmployeeInfoRestrictionGroup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{
    public interface IEmployeeInfoRestrictionGroupService : IDataService<EmployeeInfoRestrictionGroup>
    {
        AjaxViewModel ValidateCombination(CreateEditViewModel model);
        IQueryable<ListViewModel> GetList();
        bool IsCodeValid(string code, int id);
        AjaxViewModel ValidateDelete(int id);
    }
    public class EmployeeInfoRestrictionGroupProvider : IEmployeeInfoRestrictionGroupService
    {
        ChakaContext context;
        int businessGroupID = 2;
        public EmployeeInfoRestrictionGroupProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<EmployeeInfoRestrictionGroup> AllEmployeeInfoRestrictionGroup
        {
            get { return context.EmployeeInfoRestrictionGroup.AsNoTracking().Where(x => !x.DelDate.HasValue); }
        }
        public int Add(EmployeeInfoRestrictionGroup entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var infoRestrict = Get(Id);
            if(infoRestrict != null)
            {
                context.SbDelete(infoRestrict);
                context.SaveChanges();
            }
        }

        public int Edit(EmployeeInfoRestrictionGroup entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public AjaxViewModel ValidateDelete(int id)
        {
            var JsonViewModel = new AjaxViewModel();
            var result = context.User.Any(m => !m.DelDate.HasValue && m.RestrictionGroupId == id);
            JsonViewModel.IsSuccess = result;
            JsonViewModel.Message = "User";

            return JsonViewModel;
        }

        public IQueryable<EmployeeInfoRestrictionGroup> Get()
        {
            return AllEmployeeInfoRestrictionGroup;
        }

        public EmployeeInfoRestrictionGroup Get(int id)
        {
            return AllEmployeeInfoRestrictionGroup.SingleOrDefault(x => x.Id == id);   
        }

        public IQueryable<ListViewModel> GetList()
        {
            var query = from restrict in AllEmployeeInfoRestrictionGroup
                        select new ListViewModel()
                        {
                            ID = restrict.Id.ToString(),
                            Code = restrict.Code,
                            Name = restrict.Name,
                            IsActive = restrict.IsActive,
                            Description = restrict.Description,
                            BiodataAuthority = restrict.BiodataAuthority,
                            PaymentAuthority = restrict.PaymentAuthority,
                            PayrollAuthority = restrict.PayrollAuthority,
                            CustomAuthority = restrict.CustomAuthority,
                            SkillAuthority = restrict.SkillAuthority,
                            FamilyAuthority = restrict.FamilyAuthority,
                            EducationAuthority = restrict.EducationAuthority,
                            GradeAuthority = restrict.GradeAuthority,
                            JobTitelAuthority = restrict.JobTitleAuthority,
                            OrgUnitAuthority = restrict.OrgUnitAuthority,
                            StatusAuthority = restrict.StatusAuthority,
                            SalaryAuthority = restrict.SalaryAuthority,
                            LocationAuthority = restrict.LocationAuthority
                        };
            return query;
        }
        public AjaxViewModel ValidateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();
            var id = Convert.ToInt32(model.ID);
            var existedCode = context.EmployeeInfoRestrictionGroup.Any(x =>
                x.Code.ToLower() == model.Code
                && (x.Id != id)
                && !x.DelDate.HasValue
                && x.BusinessGroupId == businessGroupID);
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

        public bool IsCodeValid(string code, int id)
        {
            var result = AllEmployeeInfoRestrictionGroup.Any(x => x.Code == code && x.Id != id);
            return result;
        }
    }
}
