using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.Api.Security
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Menu, ViewModels.SystemAdmin.Menu.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.Menu.CreateEditViewModel, Menu>();

            CreateMap<Responsibility, ViewModels.SystemAdmin.Responsibility.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.Responsibility.CreateEditViewModel, Responsibility>();

            CreateMap<ResponsibilityGroup, ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditViewModel, ResponsibilityGroup>();

            CreateMap<RespGroupDetail, ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditRespGroupDetailViewModel>();
            CreateMap<ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditRespGroupDetailViewModel, RespGroupDetail>();

            CreateMap<User, ViewModels.SystemAdmin.User.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.User.CreateEditViewModel, User>();

            CreateMap<EmployeeInfoRestrictionGroup, ViewModels.SystemAdmin.EmployeeInfoRestrictionGroup.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.EmployeeInfoRestrictionGroup.CreateEditViewModel, EmployeeInfoRestrictionGroup>();

            CreateMap<EmployeeListFilter, ViewModels.SystemAdmin.EmployeeFilter.CreateEditViewModel>();
            CreateMap<ViewModels.SystemAdmin.EmployeeFilter.CreateEditViewModel, EmployeeListFilter>();

        }
    }
}
