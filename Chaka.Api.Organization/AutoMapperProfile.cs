using AutoMapper;
using Chaka.Database.Context.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.Api.Organization
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Level, ViewModels.Organization.Level.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.Level.CreateEditViewModel, Level>();

            CreateMap<GradeGroup, ViewModels.Organization.Level.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.GroupGrade.CreateEditViewModel, GradeGroup>();

            CreateMap<Grade, ViewModels.Organization.GroupGrade.CreateEditGradeDetailViewModel>();
            CreateMap<ViewModels.Organization.GroupGrade.CreateEditGradeDetailViewModel, Grade>();

            CreateMap<LocationClassification, ViewModels.Organization.LocationClassification.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.LocationClassification.CreateEditViewModel, LocationClassification>();

            CreateMap<LevelCategory, ViewModels.Organization.LevelCategory.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.LevelCategory.CreateEditViewModel, LevelCategory>();

            CreateMap<Level, Chaka.ViewModels.Organization.Level.CreateEditViewModel>();
            CreateMap<Chaka.ViewModels.Organization.Level.CreateEditViewModel, Level>();
            CreateMap<Chaka.ViewModels.Organization.GroupGrade.CreateEditViewModel, GradeGroup>();
            CreateMap<JobTitle, Chaka.ViewModels.Organization.JobTitle.CreateEditViewModel>();
            CreateMap<Chaka.ViewModels.Organization.JobTitle.CreateEditViewModel, JobTitle>();
            CreateMap<JobDescription, Chaka.ViewModels.Organization.JobTitle.CreateEditDescriptionViewModel>();
            CreateMap<Chaka.ViewModels.Organization.JobTitle.CreateEditDescriptionViewModel, JobDescription>();

            CreateMap<JobFamily, ViewModels.Organization.JobFamily.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.JobFamily.CreateEditViewModel, JobFamily>();
            CreateMap<JobMaster, ViewModels.Organization.JobMaster.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.JobMaster.CreateEditViewModel, JobMaster>();

            CreateMap<OrgUnitTransaction, ViewModels.Organization.OrgUnitChange.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.OrgUnitChange.CreateEditViewModel, OrgUnitTransaction>();
            CreateMap<OrgUnitTransaction, OrgUnit>();
            CreateMap<OrgUnit, OrgUnitTransaction>();
            CreateMap<OrgUnitTransactionDetail, ViewModels.Organization.OrgUnitChange.CreateEditDetailViewModel>();
            CreateMap<ViewModels.Organization.OrgUnitChange.CreateEditDetailViewModel, OrgUnitTransactionDetail>();

            CreateMap<OrgUnit, ViewModels.Organization.OrgUnit.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.OrgUnit.CreateEditViewModel, OrgUnit>();

            CreateMap<OrganizationListJobtitle, ViewModels.Organization.OrgUnit.CreateEditJobTitleViewModel>();
            CreateMap<ViewModels.Organization.OrgUnit.CreateEditJobTitleViewModel, OrganizationListJobtitle>();

            CreateMap<OrganizationListLocation, ViewModels.Organization.OrgUnit.CreateEditLocationViewModel>();
            CreateMap<ViewModels.Organization.OrgUnit.CreateEditJobTitleViewModel, OrganizationListLocation>();

            CreateMap<LegalEntityInformation, ViewModels.Organization.OrgUnit.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.OrgUnit.CreateEditViewModel, LegalEntityInformation>();

            CreateMap<JobStatus, ViewModels.Organization.JobStatus.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.JobStatus.CreateEditViewModel, JobStatus>();

            CreateMap<Location, ViewModels.Organization.Location.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.Location.CreateEditViewModel, Location>();

            CreateMap<LineOfBusiness, ViewModels.Organization.LineOfBusiness.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.LineOfBusiness.CreateEditViewModel, LineOfBusiness>();

            CreateMap<JobFunction, ViewModels.Organization.JobFunction.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.JobFunction.CreateEditViewModel, JobFunction>();

            CreateMap<OrganizationLevel, ViewModels.Organization.OrganizationLevel.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.OrganizationLevel.CreateEditViewModel, OrganizationLevel>();

            CreateMap<JobtitleSpecification, ViewModels.Organization.JobTitle.CreateEditSpecificationViewModel>();
            CreateMap<ViewModels.Organization.JobTitle.CreateEditSpecificationViewModel, JobtitleSpecification>();

            CreateMap<CostCenter, ViewModels.Organization.CostCenter.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.CostCenter.CreateEditViewModel, CostCenter>();

            CreateMap<Country, ViewModels.Organization.Country.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.Country.CreateEditViewModel, Country>();

            CreateMap<Province, ViewModels.Organization.Country.CreateEditProvinceViewModel>();
            CreateMap<ViewModels.Organization.Country.CreateEditProvinceViewModel, Province>();

            CreateMap<City, ViewModels.Organization.Country.CreateEditCityViewModel>();
            CreateMap<ViewModels.Organization.Country.CreateEditCityViewModel, City>();

            CreateMap<Siupclass, ViewModels.Organization.SIUPClass.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.SIUPClass.CreateEditViewModel, Siupclass>();

            CreateMap<BusinessFieldRegulation, ViewModels.Organization.BusinessFieldRegulation.CreateEditViewModel>();
            CreateMap<ViewModels.Organization.BusinessFieldRegulation.CreateEditViewModel, BusinessFieldRegulation>();

            CreateMap<BusinessFieldCategory, ViewModels.Organization.BusinessFieldRegulation.CreateEditCategoryViewModel>();
            CreateMap<ViewModels.Organization.BusinessFieldRegulation.CreateEditCategoryViewModel, BusinessFieldCategory>();

            CreateMap<BusinessFieldClassification, ViewModels.Organization.BusinessFieldRegulation.CreateEditClassificationViewModel>();
            CreateMap<ViewModels.Organization.BusinessFieldRegulation.CreateEditClassificationViewModel, BusinessFieldClassification>();

        }
    }
}
