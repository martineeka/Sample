using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.BusinessFieldRegulation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IBusinessFieldRegulationServices : IDataService<BusinessFieldRegulation>
    {
        #region Field
        IQueryable<ListBusinessFieldRegulationViewModel> GetList();
        bool ValidCode(string Code, int Id);
        bool ExistCode(string Code);
        AjaxViewModel ValidateDelete(int id);
        #endregion Field

        #region FieldCategory
        IQueryable<BusinessFieldCategory> GetCategory();
        BusinessFieldCategory GetCategory(int Id);
        IQueryable<ListBusinessFieldCategoryViewModel> GetListCategory(int BusinessFieldRegulationID);
        ListBusinessFieldCategoryViewModel GetWithRegulationName(int Id);
        int AddCategory(BusinessFieldCategory entity);
        int EditCategory(BusinessFieldCategory entity);
        void DeleteCategory(int Id);
        bool ValidCodeCategory(string Code, int Id);
        bool ExistCodeCategory(string Code);
        AjaxViewModel ValidateDeleteCategory(int id);
        #endregion FieldCategory

        #region Classification
        IQueryable<BusinessFieldClassification> GetClassification();
        BusinessFieldClassification GetClassification(int Id);
        IQueryable<ListBusinessFieldClassificationViewModel> GetListClassification(int BusinessFieldCategoryID);
        int AddClassification(BusinessFieldClassification entity);
        int EditClassification(BusinessFieldClassification entity);
        void DeleteClassification(int Id);
        bool ValidCodeClassification(BusinessFieldClassification entity);
        bool ExistCodeClassification(BusinessFieldClassification entity);
        AjaxViewModel ValidateDeleteClassification(int id);
        #endregion Classification
    }
    public class BusinessFieldRegulationProvider : IBusinessFieldRegulationServices
    {
        ChakaContext _context;
        int businessGroupID = 2;

        public BusinessFieldRegulationProvider(ChakaContext context)
        {
            _context = context;
        }

        #region BusinessFieldRegulation
        public IQueryable<BusinessFieldRegulation> AllBusinessFieldRegulations
        {
            //get { return _context.BusinessFieldRegulation.Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID); }
            get { return _context.BusinessFieldRegulation.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<BusinessFieldRegulation> Get() => AllBusinessFieldRegulations.AsNoTracking();
        public BusinessFieldRegulation Get(int Id) => AllBusinessFieldRegulations.AsNoTracking().SingleOrDefault(x => x.Id == Id);
        public IQueryable<ListBusinessFieldRegulationViewModel> GetList()
        {
            var query = from b in AllBusinessFieldRegulations
                        select new ListBusinessFieldRegulationViewModel
                        {
                            ID = b.Id.ToString(),
                            Code = b.Code,
                            Name = b.Name,
                            Description = b.Description,
                            AppointedDate = b.AppointedDate,
                            IsActive = b.IsActive
                        };
            return query;
        }

        public int Add(BusinessFieldRegulation entity)
        {
            _context.SbAdd(entity);
            return _context.SaveChanges();
        }

        public int Edit(BusinessFieldRegulation entity)
        {
            _context.SbEdit(entity);
            return _context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var entity = Get(Id);
            if (entity != null)
            {
                _context.SbDelete(entity);
                _context.SaveChanges();
            }
        }

        public bool ValidCode(string Code, int Id)
        {
            var result = AllBusinessFieldRegulations.Any(x => x.Code == Code && x.Id != Id);
            return result;
        }

        public bool ExistCode(string Code)
        {
            var result = AllBusinessFieldRegulations.Any(x => x.Code == Code);
            return result;
        }

        public AjaxViewModel ValidateDelete(int id)
        {
            var JsonViewModel = new AjaxViewModel();
            var result = _context.LegalEntityInformation.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.BusinessFieldRegulationId == id);
            JsonViewModel.IsSuccess = result;
            JsonViewModel.Message = "Legal Entity Information";
            return JsonViewModel;
        }

        #endregion BusinessFieldRegulation
        #region  BusinessFieldCategory
        public IQueryable<BusinessFieldCategory> AllBusinessFieldCategories
        {
            //get { return _context.BusinessFieldCategory.Where(a => !a.DelDate.HasValue && a.BusinessGroupId == businessGroupID); }
            get { return _context.BusinessFieldCategory.Where(a => !a.DelDate.HasValue); }
        }

        public IQueryable<BusinessFieldCategory> GetCategory() => AllBusinessFieldCategories.AsNoTracking();

        public BusinessFieldCategory GetCategory(int Id) => AllBusinessFieldCategories.AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public IQueryable<ListBusinessFieldCategoryViewModel> GetListCategory(int BusinessFieldRegulationID)
        {
            var query = from e in AllBusinessFieldCategories
                        where e.BusinessFieldRegulationId == BusinessFieldRegulationID
                        select new ListBusinessFieldCategoryViewModel
                        {
                            ID = e.Id.ToString(),
                            Code = e.Code,
                            Name = e.Name,
                            Description = e.Description,
                            IsActive = e.IsActive
                        };
            return query;
        }


        public ListBusinessFieldCategoryViewModel GetWithRegulationName(int Id)
        {
            var query = from e in AllBusinessFieldCategories
                        join pg in _context.BusinessFieldRegulation on e.BusinessFieldRegulationId equals pg.Id into pgleft
                        from pgl in pgleft.DefaultIfEmpty()
                        where e.Id == Id
                        select new ListBusinessFieldCategoryViewModel
                        {
                            ID = e.Id.ToString(),
                            Code = e.Code,
                            Name = e.Name,
                            Description = e.Description,
                            IsActive = e.IsActive,
                            RegulationName = pgl.Name
                        };
            return query.SingleOrDefault();
        }


        public int AddCategory(BusinessFieldCategory entity)
        {
            _context.SbAdd(entity);
            return _context.SaveChanges();
        }

        public int EditCategory(BusinessFieldCategory entity)
        {
            _context.SbEdit(entity);
            return _context.SaveChanges();
        }

        public void DeleteCategory(int Id)
        {
            var entity = GetCategory(Id);
            if (entity != null)
            {
                _context.SbDelete(entity);
                _context.SaveChanges();
            }
        }

        public bool ValidCodeCategory(string Code, int Id)
        {
            var result = AllBusinessFieldCategories.Any(x => x.Code == Code && x.Id != Id);
            return result;
        }
        
        public bool ExistCodeCategory(string Code)
        {
            var result = AllBusinessFieldCategories.Any(x => x.Code == Code);
            return result;
        }
        public AjaxViewModel ValidateDeleteCategory(int id)
        {
            var JsonViewModel = new AjaxViewModel();
            var result = _context.LegalEntityInformation.Any(m => !m.DelDate.HasValue
            //var result = _context.LegalEntityInformation.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.BusinessFieldCategoryId == id);
            JsonViewModel.IsSuccess = result;
            JsonViewModel.Message = "Legal Entity Information";
            return JsonViewModel;
        }
        #endregion BusinessFieldCategory

        #region Classification
        public IQueryable<BusinessFieldClassification> AllBusinessFieldClassification
        {
            //get { return _context.BusinessFieldClassification.Where(a => !a.DelDate.HasValue && a.BusinessGroupId == businessGroupID); }
            get { return _context.BusinessFieldClassification.Where(a => !a.DelDate.HasValue); }
        }

        public IQueryable<BusinessFieldClassification> GetClassification() => AllBusinessFieldClassification.AsNoTracking();

        public BusinessFieldClassification GetClassification(int Id) => AllBusinessFieldClassification.AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public IQueryable<ListBusinessFieldClassificationViewModel> GetListClassification(int BusinessFieldCategoryID)
        {
            var query = from e in AllBusinessFieldClassification
                        select new ListBusinessFieldClassificationViewModel
                        {
                            ID = e.Id.ToString(),
                            MainSectionCode = e.MainSectionCode,
                            SectionCode = e.SectionCode,
                            SubSectionCode = e.SubSectionCode,
                            GroupCode = e.GroupCode,
                            MainSectionName = e.MainSectionName,
                            SectionName = e.SectionName,
                            SubSectionName = e.SubSectionName,
                            GroupName = e.GroupName,
                            Description = e.Description,
                            IsActive = e.IsActive
                        };
            return query;
        }

        public int AddClassification(BusinessFieldClassification entity)
        {
            _context.SbAdd(entity);
            return _context.SaveChanges();
        }

        public int EditClassification(BusinessFieldClassification entity)
        {
            _context.SbEdit(entity);
            return _context.SaveChanges();
        }

        public void DeleteClassification(int Id)
        {
            var entity = GetClassification(Id);
            if (entity != null)
            {
                _context.SbDelete(entity);
                _context.SaveChanges();
            }
        }

        public bool ValidCodeClassification(BusinessFieldClassification entity)
        {
            var result = AllBusinessFieldClassification.Any(x => (
                    x.MainSectionCode == entity.MainSectionCode ||
                    x.SectionCode == entity.SectionCode ||
                    x.SubSectionCode == entity.SubSectionCode ||
                    x.GroupCode == entity.GroupCode
                ) && x.Id != entity.Id);
            return result;
        }

        public bool ExistCodeClassification(BusinessFieldClassification entity)
        {
            var result = AllBusinessFieldClassification.Any(x =>
                    x.MainSectionCode == entity.MainSectionCode ||
                    x.SectionCode == entity.SectionCode ||
                    x.SubSectionCode == entity.SubSectionCode ||
                    x.GroupCode == entity.GroupCode
                );
            return result;
        }

        public AjaxViewModel ValidateDeleteClassification(int id)
        {
            var JsonViewModel = new AjaxViewModel();
            var result = _context.LegalEntityInformation.Any(m => !m.DelDate.HasValue
            //var result = _context.LegalEntityInformation.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.BusinessFieldClassificationId == id);
            JsonViewModel.IsSuccess = result;
            JsonViewModel.Message = "Legal Entity Information";
            return JsonViewModel;
        }
        #endregion Classification
    }
}
