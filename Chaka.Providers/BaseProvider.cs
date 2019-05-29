using Chaka.Database.Context.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Reflection;
using System.Linq;
using Chaka.Utilities;

namespace Chaka.Providers
{
    public interface IDataService<T> where T : class
    {
        IQueryable<T> Get();
        T Get(int id);
        int Add(T entity);
        int Edit(T entity);
        void Delete(int Id);
    }
    public static class DbContextExtension
    {
        private static void SetValue<TEntity>(TEntity entity, string propName, object value)
        {
            var propertyInfo = entity.GetType()
                .GetProperty(propName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo != null)
                propertyInfo.SetValue(entity, value, null);
        }

        public static EntityEntry<TEntity> SbAdd<TEntity>(this ChakaContext context, TEntity entity) where TEntity : class
        {
            SetValue(entity, "CreatedBy", TokenDataClaim.CurrentUserID);
            SetValue(entity, "BusinessGroupID", TokenDataClaim.CurrentBusinessGrupID);
            SetValue(entity, "CreatedDate", DateTime.Now);

            return context.Set<TEntity>().Add(entity);
        }

        public static EntityEntry<TEntity> SbEdit<TEntity>(this ChakaContext context, TEntity entity) where TEntity : class
        {
            SetValue(entity, "UpdatedBy", 100);
            SetValue(entity, "BusinessGroupID", 2);
            SetValue(entity, "UpdatedDate", DateTime.Now);

            return context.Set<TEntity>().Update(entity);
        }

        public static EntityEntry<TEntity> SbDelete<TEntity>(this ChakaContext context, TEntity entity) where TEntity : class
        {
            SetValue(entity, "UpdatedBy", 100);
            SetValue(entity, "DelDate", DateTime.Now);

            return context.Set<TEntity>().Update(entity);
        }
    }


    public class BaseProvider
    {

    }
}
