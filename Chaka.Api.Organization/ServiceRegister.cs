using Chaka.Providers.Organization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chaka.Api.Organization
{
    public static class ServiceRegister
    {
        public static void RegisterPhoenixDependencies(this IServiceCollection services)
        {
            services.AddScoped<ILevelService, LevelProvider>();

            services.AddScoped<ILevelCategoryService, LevelCategoryProvider>();

            services.AddScoped<IGroupGradeService, GroupGradeProvider>();

            services.AddScoped<IJobTitleService, JobTitleProvider>();

            services.AddScoped<IJobStatusService, JobStatusProvider>();

            services.AddScoped<ILocationClassificationService, LocationClassificationProvider>();

            services.AddScoped<IJobFamilyService, JobFamilyProvider>();

            services.AddScoped<IJobMasterService, JobMasterProvider>();

            services.AddScoped<IBrowseOrganizationUnitProviderService, BrowseOrganizationUnitProvider>();
            services.AddScoped<IBrowseSuperiorProviderService, BrowseSuperiorProvider>();

            services.AddScoped<IOrganizationUnitChangeService, OrganizationUnitChangeProvider>();

            services.AddScoped<IOrganizationUnitService, OrganizationUnitProvider>();

            services.AddScoped<ILocationService, LocationProvider>();
             
            
            services.AddScoped<ILineOfBusinessServices, LineOfBusinessProvider>();

            services.AddScoped<IJobFunctionServices, JobFunctionProvider>();

            services.AddScoped<IOrganizationReportService, OrganizationReportProvider>();
            services.AddScoped<IOrganizationLevelService, OrganizationLevelProvider>();

            services.AddScoped<ICostCenterService, CostCenterProvider>();

            services.AddScoped<ICountryService, CountryProvider>();
            services.AddScoped<IProvinceService, ProvinceProvider>();
            services.AddScoped<ICityService, CityProvider>();

            services.AddScoped<ISIUPClassService, SIUPClassProvider>();
            services.AddScoped<IBusinessFieldRegulationServices, BusinessFieldRegulationProvider>();
        }
    }
}
