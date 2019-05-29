using AutoMapper;
using Chaka.Database.Context.Models;
using Chaka.ViewModels.SystemAdmin.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chaka.WEBAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Chaka.Database.Context.Models.Menu, Chaka.ViewModels.SystemAdmin.Menu.CreateEditViewModel>();
            CreateMap<CreateEditViewModel, Chaka.Database.Context.Models.Menu>();


            CreateMap<Responsibility, Chaka.ViewModels.SystemAdmin.Responsibility.CreateEditViewModel>();
            CreateMap<Chaka.ViewModels.SystemAdmin.Responsibility.CreateEditViewModel, Responsibility>();

            CreateMap<ResponsibilityGroup, Chaka.ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditViewModel>();
            CreateMap<Chaka.ViewModels.SystemAdmin.ResponsibilityGroup.CreateEditViewModel, ResponsibilityGroup>();
        }
    }
}
