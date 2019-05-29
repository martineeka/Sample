﻿using Chaka.Providers;
using Chaka.Providers.SystemAdmin;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.WEBAPI
{
    public static class ServiceRegister
    {
        public static void RegisterPhoenixDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMenuService, MenuProvider>();
            services.AddScoped<IResponsibilityService, ResponsibiltyProvider>();
            services.AddScoped<IResponsibilityGroupService, ResponsibilityGroupProvider>();
            services.AddScoped<IUserService, UserProvider>();
            services.AddScoped<IOrgUnitService, OrgUnitProvider>();

        }

    }
}
