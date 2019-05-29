using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.OrgUnit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IOrganizationUnitService : IDataService<OrgUnit>
    {
        IQueryable<ListOrgUnitViewModel> GetListOrganizationUnit();
        AjaxViewModel validateCombination(CreateEditViewModel model);
        bool ValidateDelete(int orgUnitID);
        IEnumerable<CostCenter> GetCostCenters();
        IEnumerable<Location> GetLocationMaster();
        IEnumerable<OrganizationLevel> GetOrganizationLevel();
        IEnumerable<OrganizationUnitCategory> GetOrganizationUnitCategory();
        IEnumerable<OrgUnit> GetParentalOrgUnits(int id, int categoryTypeID);
        IEnumerable<Currency> GetCurrency();
        IEnumerable<KppBranch> GetKppBranches();
        IEnumerable<BusinessFieldRegulation> GetBusinessFieldRegulations();
        IEnumerable<BusinessFieldCategory> GetBusinessFieldCategories(int businessFieldRegulationID);
        IEnumerable<BusinessFieldClassification> GetBusinessFieldClassifications(int businessFieldCategoryID);
        IEnumerable<CompanyBrand> GetCompanyBrand();
        IEnumerable<BusinessStatusOwnership> GetBusinessStatus();
        IEnumerable<TaxDuty> GetTaxDuty();
        IEnumerable<Funding> GetFunding();
        IEnumerable<Siupclass> GetSIUPCLasses();
        LegalEntityInformation GetLegalEntityInformation(int id);
        int AddAttachMent(Attachment entity);
        int AddLegalEntity(LegalEntityInformation entity);
        int EditAttachMent(Attachment entity);
        int EditLegalEntity(LegalEntityInformation entity);
        //CreateEditViewModel GetOrgUnits(int id);
        IQueryable<ListJobTitleViewModel> ListJobTitle(int organizationID);
        OrganizationListJobtitle GetJobTitleDetail(int Id);
        int AddJobTitle(OrganizationListJobtitle entity);
        int EditJobTitle(OrganizationListJobtitle entity);
        void DeleteJobTitle(int Id);
        void DeleteLegalEntity(int legalEntityID);
        IQueryable<ListLocationDetailViewModel> ListLocation(int organizationID);
        OrganizationListLocation GetLocationDetail(int Id);
        int AddLocation(OrganizationListLocation entity);
        int EditLocation(OrganizationListLocation entity);
        void DeleteLocation(int Id);

    }

    public class OrganizationUnitProvider : IOrganizationUnitService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;

        public OrganizationUnitProvider(ChakaContext context)
        {
            this.context = context;
        }

        #region OrgUnit
        public IQueryable<OrgUnit> Get() => context.OrgUnit.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public OrgUnit Get(int Id) => context.OrgUnit.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        //public CreateEditViewModel GetOrgUnits(int id)
        //{
        //    var query = (from org in Get()
        //                 join ou in context.OrgUnit on org.ParentId equals ou.Id
        //                 join emp in AllEmployeeBiodatas on org.SuperiorId equals emp.EmployeeId
                      
        //                 where org.Id == id
        //                 select new CreateEditViewModel
        //                 {
        //                     ID = Convert.ToString(org.Id),
        //                     Code = org.Code,
        //                     Name = org.Name,
        //                     SuperiorID = EncryptionHelper.EncryptUrlParam(Convert.ToString(org.SuperiorId)),
        //                     SuperiorName = emp.MiddleName == null
        //                        ? emp.LastName == null
        //                            ? string.Format("{0}", emp.FirstName)
        //                            : string.Format("{0} {1} ", emp.FirstName,
        //                             emp.LastName)
        //                            : string.Format("{0} {1} {2} ", emp.FirstName,
        //                        emp.MiddleName, emp.LastName),
        //                     OrganizationlevelID = org.OrganizationlevelId,
        //                     ParentID = EncryptionHelper.EncryptUrlParam(Convert.ToString(org.ParentId)),
        //                     ParentName = ou.Name,
        //                     BeginEff = org.BeginEff,
        //                     LastEff = org.LastEff,
        //                     CostCenterID = org.CostCenterId,
        //                     CategoryID = org.CategoryId,
        //                     IsLegalEntity = org.IsLegalEntity
        //                 }).FirstOrDefault();

        //    return query;
        //}

        public IQueryable<OrgUnit> AllOrganizationUnits
        {
            get { return context.OrgUnit.Where(orgUnit => !orgUnit.DelDate.HasValue && (orgUnit.BeginEff <= DateTime.Now && orgUnit.LastEff == null || orgUnit.LastEff >= DateTime.Now)); }
        }
        public IQueryable<EmployeeBiodata> AllEmployeeBiodatas
        {
            get { return context.EmployeeBiodata.Where(orgUnit => !orgUnit.DelDate.HasValue && (orgUnit.BeginEff < DateTime.Now && orgUnit.LastEff == null || orgUnit.LastEff >= DateTime.Now)); }
        }

        public IQueryable<ListOrgUnitViewModel> GetListOrganizationUnit()
        {
            var query = from orgUnit in AllOrganizationUnits
                        join orgLv in context.OrganizationLevel on orgUnit.OrganizationlevelId equals orgLv.Id
                        join emp in AllEmployeeBiodatas on orgUnit.SuperiorId equals emp.EmployeeId
                        into list from y1 in list.DefaultIfEmpty()
                        select new ListOrgUnitViewModel
                        {
                            ID = orgUnit.Id.ToString(),
                            Code = orgUnit.Code,
                            Name = orgUnit.Name,
                            CostCenterName = orgUnit.CostCenter.Name,
                            Description = orgUnit.Description,
                            SuperiorName = 
                                y1.MiddleName == null
                                ?y1.LastName == null
                                    ?string.Format("{0}",y1.FirstName)
                                    : string.Format("{0} {1} ", y1.FirstName,
                                     y1.LastName)
                                    : string.Format("{0} {1} {2} ", y1.FirstName,
                                y1.MiddleName, y1.LastName),
                            OrganizationUnitLevel = orgLv.Name,
                            BeginEff = orgUnit.BeginEff,
                            LastEff = orgUnit.LastEff,
                            ParentOrgUnit = orgUnit.Parent.Name,
                            //Category= orgUnit.Category.Name,
                            IsLegalEntity = orgUnit.IsLegalEntity
                        };

            return query;
        }

        public int Add(OrgUnit entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int AddLegalEntity(LegalEntityInformation entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }
        public int AddAttachMent(Attachment entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        //public string GetTableName(dynamic objectName)
        //{
        //    //var tableName = context.Metadata.PersistentTypes.FirstOrDefault(typ => typ.FullName == objectName.GetType().FullName);
        //    //return tableName != null ? tableName.Table.Name : null;

        //    var entityName = objectName.GetType().Name;
        //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
        //    var storageMetadata = objectContext.MetadataWorkspace.GetItems<EntityContainerMapping>(DataSpace.CSSpace);

        //    foreach (var ecm in storageMetadata)
        //    {
        //        EntitySet entitySet;
        //        if (ecm.StoreEntityContainer.TryGetEntitySetByName(entityName, true, out entitySet))
        //        {
        //            return entitySet.Name;
        //        }
        //    }

        //    return null;
        //}

        public void Delete(int Id)
        {
            var OrgUnit = Get(Id);
            if (OrgUnit != null)
            {
                context.SbDelete(OrgUnit);
                context.SaveChanges();
            }
        }

        public void DeleteLegalEntity(int legalEntityID)
        {
            var legalEntity = context.LegalEntityInformation.SingleOrDefault(entity => entity.Id == legalEntityID);
            if (legalEntity != null)
            {
                context.SbDelete(legalEntity);
                context.SaveChanges();
            }
        }

        public int Edit(OrgUnit entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public int EditLegalEntity(LegalEntityInformation entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }
        public int EditAttachMent(Attachment entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public IEnumerable<OrganizationUnitCategory> GetOrganizationUnitCategory()
        {
            var query = context.OrganizationUnitCategory;
            return query; ; //.Where(cat => !cat.Name.Contains("Business"));
        }

        public IEnumerable<OrgUnit> GetOrgUnits()
        {
            var query = AllOrganizationUnits.Where(o => (DateTime.Today >= o.BeginEff && DateTime.Today <= o.LastEff) || o.LastEff == null).OrderBy(orgUnit => orgUnit.Name);
            return query.ToList();
        }
        public IEnumerable<OrgUnit> GetParentalOrgUnits(int id, int categoryTypeID)
        {

            var query = categoryTypeID == 1 ? AllOrganizationUnits.Where(orgUnit => orgUnit.Id != id && orgUnit.CategoryId != 2) : categoryTypeID == 2 ? AllOrganizationUnits.Where(orgUnit => orgUnit.Id != id) : AllOrganizationUnits.Where(orgUnit => orgUnit.Id != id && orgUnit.CategoryId != categoryTypeID);
            return query.ToList().OrderBy(orgUnit => orgUnit.Name);
            return null;
        }

        public LegalEntityInformation GetLegalEntityInformation(int id)
        {
            var query = context.LegalEntityInformation.SingleOrDefault(entity => entity.Id == id);

            return query;
        }

        public IEnumerable<OrgUnit> GetCompanies()
        {
            var query = GetOrgUnits().Where(x => x.ParentId == null);
            return query.ToList();
        }

        #region Organization unit Level
        public IQueryable<OrganizationLevel> AllOrganizationLevels
        {
            get
            {
                return context.OrganizationLevel.Where(orgLvl => !orgLvl.DelDate.HasValue && orgLvl.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<OrganizationLevel> GetOrganizationLevel()
        {
            var query = AllOrganizationLevels;
            return query.ToList().OrderBy(orgLvl => orgLvl.Name);
        }
        #endregion

        #region Cost Center
        public IQueryable<CostCenter> AllCostCenters
        {
            get
            {
                var query = context.CostCenter.Where(center => center.BusinessGroupId == 2 && !center.DelDate.HasValue);
                return query;
            }
        }

        public IEnumerable<CostCenter> GetCostCenters()
        {
            var query = context.CostCenter.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }
        #endregion

        #region DropDown Legal Entity
        public IQueryable<Currency> AllCurrency
        {
            get
            {
              return context.Currency.Where(orgLvl => !orgLvl.DelDate.HasValue && orgLvl.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<Currency> GetCurrency()
        {
            var query = AllCurrency.Where(currency => currency.IsActive).OrderBy(crncy => crncy.Name);
            return query.ToList();
        }

        public IQueryable<KppBranch> AllKppBranches
        {
            get
            {
                return context.KppBranch.Where(kppBranch => !kppBranch.DelDate.HasValue && kppBranch.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<KppBranch> GetKppBranches()
        {
            var query = AllKppBranches.Where(kppBranch => kppBranch.IsActive);
            return query.ToList().OrderBy(kppBranch => kppBranch.Name);
        }

        public IQueryable<BusinessFieldRegulation> AllBusinessFieldRegulations
        {
            get
            {
                return context.BusinessFieldRegulation.Where(regulation => !regulation.DelDate.HasValue && regulation.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<BusinessFieldRegulation> GetBusinessFieldRegulations()
        {
            var query = AllBusinessFieldRegulations.Where(regulation => regulation.IsActive);
            return query.ToList();
        }

        public IQueryable<BusinessFieldCategory> AllBusinessFieldCategories
        {
            get
            {
                return context.BusinessFieldCategory.Where(category => !category.DelDate.HasValue && category.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<BusinessFieldCategory> GetBusinessFieldCategories(int businessFieldRegulationID)
        {
            var query = AllBusinessFieldCategories.Where(category => category.IsActive && category.BusinessFieldRegulationId == businessFieldRegulationID);
            return query.ToList();
        }

        public IQueryable<BusinessFieldClassification> AllBusinessFieldClassifications
        {
            get
            {
                return context.BusinessFieldClassification.Where(classification => !classification.DelDate.HasValue && classification.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<BusinessFieldClassification> GetBusinessFieldClassifications(int businessFieldCategoryID)
        {
            var query = AllBusinessFieldClassifications.Where(classification => classification.IsActive && classification.BusinessFieldCategoryId == businessFieldCategoryID);
            return query.ToList();
        }

        public IEnumerable<CompanyBrand> GetCompanyBrand()
        {
            return context.CompanyBrand;
        }

        public IEnumerable<BusinessStatusOwnership> GetBusinessStatus()
        {
            return context.BusinessStatusOwnership;
        }

        public IEnumerable<Funding> GetFunding()
        {
            return context.Funding;
        }

        public IEnumerable<TaxDuty> GetTaxDuty()
        {
            return context.TaxDuty;
        }

        public IQueryable<Siupclass> AllSIUPClass
        {
            get
            {
                return context.Siupclass.Where(siup => !siup.DelDate.HasValue && siup.BusinessGroupId == businessGroupID);
            }
        }

        public IEnumerable<Siupclass> GetSIUPCLasses()
        {
            var query = AllSIUPClass.Where(siupClass => siupClass.IsActive);
            return query.ToList();
        }
        #endregion

        public AjaxViewModel validateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = context.OrgUnit.Any(lv =>
                lv.Code.ToLower() == model.Code.ToLower()
                && (lv.Id != id)
                && !lv.DelDate.HasValue
                && lv.BusinessGroupId == businessGroupID);

            if (existedCode)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = " Organization Unit Code has been used ";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        public bool ValidateDelete(int id)
        {
            var usedByDescription = context.JobDescription.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.JobtitleId == id);
            var usedByRequirement = context.JobtitleRequirement.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.JobtitleId == id);
            //var usedByCandidateApplication = context.CandidateApplication.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ApplyFor == jobTitleID);
            //var usedByMedicalCheckup = context.MedicalCheckup.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ForJobTitleID == jobTitleID);
            //var usedByRecruitmentTest = context.RecruitmentTest.Any(m => m.BusinessGroupID == CurrentBusinessGroupId && !m.DelDate.HasValue
            //    && m.ForJobTitleID == jobTitleID);
            //var usedByProjectActivityVacancy = context.ProjectActivityVacancy.Any(m => m.BusinessGroupID == CurrentBusinessGroupId
            //    && m.JobTitleID == jobTitleID);

            var used = usedByDescription || usedByRequirement;
            //var used = usedByCandidateApplication || usedByMedicalCheckup || usedByProjectActivityVacancy || usedByRecruitmentTest;
            return used;

        }
        #endregion

        #region ListJobTitle
        public IQueryable<OrganizationListJobtitle> AllJobTitles
        {
            get { return context.OrganizationListJobtitle.Where(x => !x.DelDate.HasValue); }
        }
        public OrganizationListJobtitle GetJobTitleDetail(int Id) => context.OrganizationListJobtitle.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public OrganizationListJobtitle GetJobTitleDetail(int organizationID, int Id) => context.OrganizationListJobtitle.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id && x.OrganizationId == organizationID);

        public IQueryable<ListJobTitleViewModel> ListJobTitle(int organizationID)
        {
            var query = from data in AllJobTitles
                        where data.OrganizationId == organizationID
                        select new ListJobTitleViewModel()
                        {
                            ID = data.Id.ToString(),
                            Name = data.JobTitle.Name,
                            ValidFrom = data.BeginEff,
                            ValidTo = data.LastEff
                        };
            return query;
        }

        public int AddJobTitle(OrganizationListJobtitle entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int EditJobTitle(OrganizationListJobtitle entity)
        {

            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void DeleteJobTitle(int Id)
        {
            var jobTitle = GetJobTitleDetail(Id);
            if (jobTitle != null)
            {
                context.SbDelete(jobTitle);
                context.SaveChanges();
            }
        }

      
        #endregion

        #region ListLocation
        public IQueryable<OrganizationListLocation> AllLocations
        {
            get { return context.OrganizationListLocation.Where(x => !x.DelDate.HasValue); }
        }
        public OrganizationListLocation GetLocationDetail(int Id) => context.OrganizationListLocation.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public OrganizationListLocation GetLocationDetail(int organizationID, int Id) => context.OrganizationListLocation.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id && x.OrganizationId == organizationID);

        public IQueryable<ListLocationDetailViewModel> ListLocation(int organizationID)
        {
            var query = from data in AllLocations
                        where data.OrganizationId == organizationID
                        select new ListLocationDetailViewModel()
                        {
                            ID = data.Id.ToString(),
                            Name = data.Location.Name,
                            ValidFrom = data.BeginEff,
                            ValidTo = data.LastEff
                        };
            return query;
        }

        public int AddLocation(OrganizationListLocation entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int EditLocation(OrganizationListLocation entity)
        {

            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void DeleteLocation(int Id)
        {
            var location = GetLocationDetail(Id);
            if (location != null)
            {
                context.SbDelete(location);
                context.SaveChanges();
            }
        }

        public IQueryable<Location> AllLocationMaster
        {
            get
            {
                var query = context.Location.Where(center => center.BusinessGroupId == 2 && !center.DelDate.HasValue);
                return query;
            }
        }

        public IEnumerable<Location> GetLocationMaster()
        {
            var query = context.Location.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;

        }

        #endregion

    }

}
