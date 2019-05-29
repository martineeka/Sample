using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels;
using Chaka.ViewModels.Organization.OrgUnitChange;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface IOrganizationUnitChangeService : IDataService<OrgUnitTransaction>
    {
        IEnumerable<OrgUnit> GetOrgUnitChild(int parentID);
        OrgUnit GetOrgUnit(int id);
        EmployeeBiodata GetActiveEmployeeBiodata(int employeeID);

        IEnumerable<CostCenter> GetCostCenter();
        IEnumerable<OrganizationLevel> GetOrgLevel();
        IEnumerable<LegalEntityInformation> GetLegalEntityInformation();
        IEnumerable<OrganizationUnitCategory> GetCategory();

        int AddChildOrgUnit(OrgUnitTransaction orgChange, IEnumerable<OrgUnit> child);
        CreateEditViewModel GetOrgUnitTrans(int id);
        IEnumerable<ListOrgUnitChangeViewModel> List();
        bool ValidateDelete(int id);
        AjaxViewModel ValidateCombination(CreateEditViewModel model);

        IndexDetailViewModel GetDetailIndex(int orgChangeID);
        IQueryable<OrgUnitTransactionDetail> GetOrgChangeDetail();
        OrgUnitTransactionDetail GetOrgChangeDetail(int Id);
        IEnumerable<ListOrgUnitChangeDetailViewModel> GetDetailList(int orgChangeID);
        int AddOrgUnitChangeDetail(OrgUnitTransactionDetail entity);
        int EditOrgUnitChangeDetail(OrgUnitTransactionDetail entity);
        void DeleteOrgUnitChangeDetail(int id);
        int ChangeIsCurrentUsed(int orgUnitID);
        int DeadActiveDetail(OrgUnitTransactionDetail entity);
        bool IsOrgUnitChangeCodeValid(string code, int id);
        AjaxViewModel ValidateOrgUnitChangeDetail(CreateEditDetailViewModel model);
        bool ValidateDeleteOrgUnitChangeDetail(int id);
    }

    public class OrganizationUnitChangeProvider : IOrganizationUnitChangeService
    {
        readonly ChakaContext context;
        int businessGroupID = 2;

        public OrganizationUnitChangeProvider(ChakaContext context)
        {
            this.context = context;
        }

        #region query
        public IQueryable<OrgUnit> AllOrgUnit()
        {
            return context.OrgUnit.Where(org => !org.DelDate.HasValue && org.BusinessGroupId == businessGroupID);
        }

        public IQueryable<EmployeeBiodata> AllBiodata()
        {
            return context.EmployeeBiodata.Where(org => !org.DelDate.HasValue && org.BusinessGroupId == businessGroupID);
        }

        public IQueryable<EmployeeBiodata> AllCurrentBiodata()
        {
            return AllBiodata().Where(biodata => biodata.BeginEff <= DateTime.Today && (biodata.LastEff >= DateTime.Today || !biodata.LastEff.HasValue));
        }
        #endregion query

        #region Additional
        public OrgUnit GetOrgUnit(int id)
        {
            return AllOrgUnit().SingleOrDefault(org => org.Id == id);
        }

        public EmployeeBiodata GetActiveEmployeeBiodata(int employeeID)
        {
            return context.EmployeeBiodata.SingleOrDefault(e => e.EmployeeId == employeeID
                                                            && e.BusinessGroupId == businessGroupID
                                                            && !e.DelDate.HasValue
                                                            && e.BeginEff <= DateTime.Today
                                                            && (e.LastEff >= DateTime.Today || e.LastEff == null));
        }
        #endregion Additional

        #region DropDownList

        public IEnumerable<OrgUnit> GetOrgUnitChild(int parentID)
        {
            return AllOrgUnit().Where(org => org.ParentId == parentID);
        }
        public IEnumerable<CostCenter> GetCostCenter()
        {
            var query = context.CostCenter.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }

        public IEnumerable<OrganizationLevel> GetOrgLevel()
        {
            var query = context.OrganizationLevel.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }

        public IEnumerable<LegalEntityInformation> GetLegalEntityInformation()
        {
            var query = context.LegalEntityInformation.Where(js => !js.DelDate.HasValue && js.BusinessGroupId == businessGroupID);
            return query;
        }

        public IEnumerable<OrganizationUnitCategory> GetCategory()
        {
            var query = context.OrganizationUnitCategory;
            return query;
        }

        #endregion DropDownList


        #region Organization Unit Change
        public IQueryable<OrgUnitTransaction> Get() => context.OrgUnitTransaction.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);

        public OrgUnitTransaction Get(int Id) => context.OrgUnitTransaction.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);

        public CreateEditViewModel GetOrgUnitTrans(int id)
        {
            var query = (from org in Get()
                         join ou in context.OrgUnit on org.OrgUnitId equals ou.Id
                         where org.Id == id
                         select new CreateEditViewModel
                         {
                             ID = Convert.ToString(org.Id),
                             OrganizationUnitID = EncryptionHelper.EncryptUrlParam(Convert.ToString(org.OrgUnitId)),
                             OrganizationUnit = ou.Name,
                             BeginEff = org.BeginEff,
                             LastEff = org.LastEff,
                             Reason = org.Reason
                         }).FirstOrDefault();

            return query;
        }

        public IEnumerable<ListOrgUnitChangeViewModel> List()
        {
            var query = from jt in Get()
                        join org in context.OrgUnit on jt.OrgUnitId equals org.Id
                        select new ListOrgUnitChangeViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(jt.Id)),
                            TanggalRequest = jt.RequestDate,
                            OrgUnit = org.Name,
                            StartDate = jt.BeginEff,
                            EndDate = jt.LastEff,
                            Reason = jt.Reason,
                        };

            return query;
        }

        public int Add(OrgUnitTransaction entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int AddChildOrgUnit(OrgUnitTransaction orgChange, IEnumerable<OrgUnit> child)
        {
            foreach (var item in child)
            {
                OrgUnitTransactionDetail detail = new OrgUnitTransactionDetail();
                detail.OrgUnitTransactionId = orgChange.Id;
                detail.OrgUnitId = item.Id;
                detail.Code = item.Code;
                detail.Name = item.Name;
                detail.OrganizationleveId = item.OrganizationlevelId;
                detail.SuperiorId = item.SuperiorId;
                detail.CostCenterId = item.CostCenterId;
                detail.Description = item.Description;
                detail.BeginEff = item.BeginEff;
                detail.LastEff = item.LastEff;
                detail.ParentId = item.ParentId;
                detail.IsLegalEntity = item.IsLegalEntity;
                detail.LegalEntityInformationId = item.LegalEntityInformationId;
                detail.BusinessGroupId = item.BusinessGroupId;
                detail.StatusId = 0;
                detail.CategoryId = item.CategoryId;
                detail.CreatedBy = orgChange.CreatedBy;
                detail.CreatedDate = orgChange.CreatedDate;

                context.OrgUnitTransactionDetail.Add(detail);
            }

            return context.SaveChanges();
        }

        public int Edit(OrgUnitTransaction entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int id)
        {
            var jobTitle = Get(id);
            if (jobTitle != null)
            {
                context.SbDelete(jobTitle);
                context.SaveChanges();
            }
        }

        public bool ValidateDelete(int id)
        {
            var usedByTransactionDetail = context.OrgUnitTransactionDetail.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.OrgUnitTransactionId == id);

            var used = usedByTransactionDetail;
            //var used = usedByCandidateApplication || usedByMedicalCheckup || usedByProjectActivityVacancy || usedByRecruitmentTest;
            return used;

        }

        public AjaxViewModel ValidateCombination(CreateEditViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedOrgUnit = context.OrgUnitTransaction.Any(jt =>
                jt.OrgUnitId == Convert.ToInt32(EncryptionHelper.DecryptUrlParam(model.OrganizationUnitID))
                && (jt.Id != id)
                && !jt.DelDate.HasValue
                && jt.BusinessGroupId == businessGroupID);

            if (existedOrgUnit)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Organization Unit has been used";
            }
            else if (model.BeginEff == null)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Start Date should be filled";
            }
            else if (model.Reason == null)
            {
                viewModel.IsSuccess = false;
                viewModel.Message = "Reason should be filled";
            }
            else
            {
                viewModel.IsSuccess = true;
                viewModel.Message = "Success";
            }
            return viewModel;
        }

        #endregion Organization Unit Change

        #region Organization Unit Change Detail
        public IndexDetailViewModel GetDetailIndex(int orgChangeID)
        {
            var query = (from chg in Get()
                         join orgUn in context.OrgUnit on chg.OrgUnitId equals orgUn.Id
                         where chg.Id == orgChangeID
                         let requestDate = chg.RequestDate == null ? "N/A" : string.Format("{0:dd MMM yyyy}", chg.RequestDate)
                         let startDate = chg.BeginEff == null ? "N/A" : string.Format("{0:dd MMM yyyy}", chg.BeginEff)
                         let endDate = chg.LastEff == null ? "N/A" : string.Format("{0:dd MMM yyyy}", chg.LastEff)

                         select new IndexDetailViewModel
                         {
                             ID = chg.Id,
                             TanggalRequest = requestDate,
                             OrgUnit = orgUn.Name,
                             StartDate = startDate,
                             EndDate = endDate,
                             Reason = chg.Reason,
                         }).FirstOrDefault();

            return query;
        }

        public IQueryable<OrgUnitTransactionDetail> GetOrgChangeDetail() => context.OrgUnitTransactionDetail.AsNoTracking().Where(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID);

        //public OrgUnitTransactionDetail GetOrgChangeDetail(int Id) => context.OrgUnitTransactionDetail.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);
        public OrgUnitTransactionDetail GetOrgChangeDetail(int Id)
        {
            return context.OrgUnitTransactionDetail.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.BusinessGroupId == businessGroupID && x.Id == Id);
        }


        public IEnumerable<ListOrgUnitChangeDetailViewModel> GetDetailList(int orgChangeID)
        {
            var query = from des in GetOrgChangeDetail()
                        join bio in context.EmployeeBiodata on des.SuperiorId equals bio.EmployeeId
                        join orgLvl in context.OrganizationLevel on des.OrganizationleveId equals orgLvl.Id
                        join cat in context.OrganizationUnitCategory on des.CategoryId equals cat.Id
                        join sts in context.OrgUnitChangeStatus on des.StatusId equals sts.Id
                        join orgUn in context.OrgUnit on des.ParentId equals orgUn.Id
                        where des.OrgUnitTransactionId == orgChangeID && des.IsCurrentUsed == true
                        select new ListOrgUnitChangeDetailViewModel
                        {
                            ID = EncryptionHelper.EncryptUrlParam(Convert.ToString(des.Id)),
                            OrgUnitID = des.OrgUnitId,
                            Code = des.Code,
                            Name = des.Name,
                            CostCenter = des.CostCenter.Name,
                            Description = des.Description,
                            Superior = bio.MiddleName == null ?
                                       bio.LastName == null ? string.Format("{0}", bio.FirstName)
                                                 : string.Format("{0} {1}", bio.FirstName, bio.LastName)
                                                 : string.Format("{0} {1} {2}", bio.FirstName, bio.MiddleName, bio.LastName),
                            OrganizationLevel = orgLvl.Name,
                            BeginEff = des.BeginEff,
                            LastEff = des.LastEff,
                            ParentOrgUnit = orgUn.Name,
                            Category = cat.Name,
                            IsLegalEntity = des.IsLegalEntity == true ? "Yes" : "No",
                            Status = sts.Name
                        };
            return query;
        }


        public int AddOrgUnitChangeDetail(OrgUnitTransactionDetail entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int EditOrgUnitChangeDetail(OrgUnitTransactionDetail entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public void DeleteOrgUnitChangeDetail(int id)
        {
            var jobTitle = GetOrgChangeDetail(id);
            if (jobTitle != null)
            {
                jobTitle.StatusId = OrgChangeStatus.NotActive;
                context.SbDelete(jobTitle);
                context.SaveChanges();
            }
        }

        public int ChangeIsCurrentUsed(int orgUnitID)
        {
            var getDetail = context.OrgUnitTransactionDetail.Where(fil => fil.OrgUnitId == orgUnitID && fil.IsCurrentUsed == true && !fil.DelDate.HasValue && fil.BusinessGroupId == businessGroupID);

            foreach (var item in getDetail)
            {
                item.IsCurrentUsed = false;
            }
            return context.SaveChanges();
        }

        public int DeadActiveDetail(OrgUnitTransactionDetail entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public bool IsOrgUnitChangeCodeValid(string code, int id)
        {
            var result = GetOrgChangeDetail().Any(jt => jt.Code == code && jt.Id != id);
            return result;
        }

        public AjaxViewModel ValidateOrgUnitChangeDetail(CreateEditDetailViewModel model)
        {
            AjaxViewModel viewModel = new AjaxViewModel();

            var id = Convert.ToInt32(model.ID);
            var existedCode = GetOrgChangeDetail().Any(jt =>
                            jt.Code == model.Code.ToLower()
                            && jt.Id != id
                            && !jt.DelDate.HasValue
                            && jt.BusinessGroupId == businessGroupID);

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

        public bool ValidateDeleteOrgUnitChangeDetail(int id)
        {
            var usedByTransactionDetailJobTitle = context.OrgUnitTransDetJobTitle.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
                && m.OrgUnitTransactionDetId == id);
            var usedByTransactionDetailLocation = true;
            //var usedByTransactionDetailLocation = context.OrgUnitTransDetLocation.Any(m => m.BusinessGroupId == businessGroupID && !m.DelDate.HasValue
            //    && m.OrgUnitTransactionDetId == id);

            var used = usedByTransactionDetailJobTitle || usedByTransactionDetailLocation;
            return used;

        }
        #endregion Organization Unit Change Detail
    }
}
