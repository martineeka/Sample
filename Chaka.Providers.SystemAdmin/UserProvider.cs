using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels.Browse;
using Chaka.ViewModels.SystemAdmin.Account;
using Chaka.ViewModels.SystemAdmin.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{
    public interface IUserService : IDataService<User>
    {
        User ValidateLogin(LoginViewModel model);
        int EditCurrentBussinessGroupID(int CurrentBussinessGroupID, int UserID);


        #region User
        IQueryable<ListUserViewModel> GetList();
        IQueryable<InfoEmployeeViewDetail> GetEmployee();
        IQueryable<EmployeeListFilter> GetEmployeeListFilter();
        IQueryable<ResponsibilityGroup> GetResponsibilityGroup();
        IQueryable<EmployeeInfoRestrictionGroup> GetRestrictionGroup();
        IQueryable<UserStatus> GetUserStatus();
        IQueryable<PreferenceGroup> GetPreferenceGroup();
        bool UserValidateCombination(User model);
        IEnumerable<BrowseEmployeeViewModel> GetListEmployee();
        CreateEditViewModel GetDetail(int Id);
        #endregion User
    }

    public class UserProvider : IUserService
    {
        readonly ChakaContext context;
        protected readonly string cryptographyKey = "IGLO2015";
        int businessGroupID = 2;

        public UserProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<User> AllUsers
        {
            get { return context.User.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Employee> AllEmployees
        {
            get { return context.Employee.Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID); }
        }

        public User ValidateLogin(LoginViewModel model)
        {
            string encryptedPassword = RijndaelHelper.Encrypt(model.Password, cryptographyKey);

            var user = new User();
            //get username with loginname
            var currentUser = AllUsers.SingleOrDefault(us => us.LoginName == model.LoginName && !us.DelDate.HasValue);
            if (currentUser != null)
            {
                if (currentUser.Password == encryptedPassword)
                {
                   user = AllUsers.SingleOrDefault(x => x.UserName == currentUser.UserName && x.Password == encryptedPassword);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
            return user;
        }

        public IQueryable<User> Get() => AllUsers.AsNoTracking();

        public User Get(int Id) => AllUsers.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public int EditCurrentBussinessGroupID(int CurrentBussinessGroupID, int UserID)
        {
            var entity = context.User.FirstOrDefault(m => m.Id == UserID);
            if (entity != null)
            {
                entity.CurrentBusinessGroupId = CurrentBussinessGroupID;
                return context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<BrowseEmployeeViewModel> GetListEmployee()
        {
            var query = from e in AllEmployees
                        join eb in context.EmployeeBiodata on e.Id equals eb.EmployeeId into ebMg
                            from ebleft in ebMg.DefaultIfEmpty()
                        select new BrowseEmployeeViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(e.Id)),
                            NIK = e.Nik,
                            Employee = ebleft.FirstName+" "+ ebleft.MiddleName+" "+ ebleft.LastName,
                            Gender = ebleft.Gender,
                            BirthDate = ebleft.BirthDate
                        };

            return query;
        }

        public CreateEditViewModel GetDetail(int Id)
        {
            var query = from u in AllUsers
                        join e in context.Employee on u.EmployeeId equals e.Id into eMg
                            from eleft in eMg.DefaultIfEmpty()
                        join eb in context.EmployeeBiodata on eleft.Id equals eb.EmployeeId into ebMg
                            from ebleft in ebMg.DefaultIfEmpty()
                        where u.Id == Id
                        select new CreateEditViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(u.Id)),
                            LoginName = u.LoginName,
                            UserName = u.UserName,
                            Email = u.Email,
                            EmployeeID = EncryptionHelper.EncryptUrlParam(Convert.ToString(eleft.Id)),
                            NIK = eleft.Nik,
                            Password = u.Password,
                            Employee = ebleft.FirstName + " " + ebleft.MiddleName + " " + ebleft.LastName,
                            EmployeeListFilterID = u.EmployeeListFilterId.ToString(),
                            ResponsibilityGroupID = u.ResponsibilityGroupId.ToString(),
                            PreferenceGroupID = u.PreferenceGroupId.ToString(),
                            RestrictionGroupID = u.RestrictionGroupId.ToString(),
                            UserStatusID = u.UserStatusId.ToString()
                        };

            return query.AsNoTracking().SingleOrDefault();
        }

        #region User

        public IQueryable<ListUserViewModel> GetList()
        {
            var query = from u in AllUsers
                        join e in context.Employee on u.EmployeeId equals e.Id into eMg
                            from eleft in eMg.DefaultIfEmpty()
                        join uStat in context.UserStatus on u.UserStatusId equals uStat.Id
                        join elf in context.EmployeeListFilter on u.EmployeeListFilterId equals elf.Id into elfMg
                            from elfleft in elfMg.DefaultIfEmpty()
                        join eb in context.EmployeeBiodata on eleft.Id equals eb.EmployeeId into ebMg
                            from ebleft in ebMg.DefaultIfEmpty()
                        select new ListUserViewModel
                        {
                            ID = u.Id.ToString(),
                            UserName = u.UserName,
                            Email = u.Email,
                            Employee = ebleft.FirstName + " " + ebleft.MiddleName + " " + ebleft.LastName,
                            Nik = eleft.Nik,
                            Filter = elfleft.Name,
                            Status = uStat.Name
                        };
            return query;
        }

        public int Add(User entity)
        {
            entity.CurrentBusinessGroupId = businessGroupID;
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(User entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = Get(Id);
            if(entity != null)
            {
                context.SbDelete(entity);
                context.SaveChanges();
            }
        }

        public IQueryable<InfoEmployeeViewDetail> GetEmployee()
        {
            var query = from e in context.Employee
                        join eb in context.EmployeeBiodata on e.Id equals eb.EmployeeId into ebMg
                        from ebleft in ebMg.DefaultIfEmpty()
                        where e.BusinessGroupId == businessGroupID
                        where !e.DelDate.HasValue
                        select new InfoEmployeeViewDetail
                        {
                            Id = e.Id,
                            Name = ebleft.FirstName+" "+ebleft.MiddleName+" "+ebleft.LastName
                        };
            return query;
        }
        public IQueryable<EmployeeListFilter> GetEmployeeListFilter() => context.EmployeeListFilter.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<ResponsibilityGroup> GetResponsibilityGroup() => context.ResponsibilityGroup.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<PreferenceGroup> GetPreferenceGroup() => context.PreferenceGroup.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<EmployeeInfoRestrictionGroup> GetRestrictionGroup() => context.EmployeeInfoRestrictionGroup.AsNoTracking().Where(x => x.BusinessGroupId == businessGroupID && !x.DelDate.HasValue);
        public IQueryable<UserStatus> GetUserStatus() => context.UserStatus.AsNoTracking();

        public bool UserValidateCombination(User model)
        {
            var id = model.Id;
            bool check;
            if (id < 1)
            {
                check = context.User.Any(jt =>
                    jt.UserName == model.UserName
                    && jt.Email == model.Email
                    && jt.EmployeeId == model.EmployeeId
                    && !jt.DelDate.HasValue);
            }
            else
            {
                check = context.User.Any(jt =>
                    jt.UserName == model.UserName
                    && jt.Email == model.Email
                    && jt.EmployeeId == model.EmployeeId
                    && (jt.Id != id)
                    && !jt.DelDate.HasValue);
            }
            return check;
        }
        #endregion User
    }
}
