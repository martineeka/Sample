using Chaka.Database.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{
    public interface IMainMenuProviderService 
    {
        User GetUser(string userName);
        IEnumerable<Responsibility> GetUserResponsibilities(string userName, DateTime? effectiveDate = null);
    }
    public class MainMenuProvider : IMainMenuProviderService
    {
        readonly ChakaContext context;

        public MainMenuProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<User> AllUser
        {
            get
            {
                return context.User.Where(m => !m.DelDate.HasValue);
            }
        }

        public IQueryable<UserResponsibility> AllUserResponsibility
        {
            get
            {
                return context.UserResponsibility.Where(m => !m.DelDate.HasValue);
            }
        }

        public IQueryable<Responsibility> AllResponsibilities
        {
            get
            {
                return context.Responsibility.Where(m => !m.DelDate.HasValue);
            }
        }

        public User GetUser(string userName)
        {
              return AllUser.SingleOrDefault(user => user.UserName == userName && !user.DelDate.HasValue);
        }

        public IEnumerable<Responsibility> GetUserResponsibilities(string userName, DateTime? effectiveDate = null)
        {
            var user = GetUser(userName);
            var query = from userResp in AllUserResponsibility
                        join resp in AllResponsibilities
                            on userResp.ResponsibilityId equals resp.Id
                        orderby resp.Name
                        where userResp.UserId == user.Id
                              && userResp.IsActive
                              && resp.IsActive
                              && resp.DefaultBusinessGroupId == user.CurrentBusinessGroupId
                        select resp;
            return query;
        }
    }
}
