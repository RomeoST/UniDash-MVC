using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DashBoard.Model.Models;
using DashBoard.Models;

namespace DashBoard.Mapping
{
    public class DomainToViewMappingProfile : Profile
    {
        public DomainToViewMappingProfile()
        {
            CreateMap<ClientProfile, UsersViewModel>()
            .ForMember(s1 => s1.Login, s2 => s2.MapFrom(s3 => s3.DutUser.UserName));

            CreateMap<DutUser, EditUserFormModel>()
                .ForMember(dest => dest.FullName,
                    input => input.MapFrom(i => i.ClientProfile.FullName))
                .ForMember(dest => dest.Login, input => input.MapFrom(i => i.UserName))
                .ForMember(dest => dest.Phone, input => input.MapFrom(i => i.PhoneNumber));

            CreateMap<ClientProfile, AccessEditFormModel>()
                .ForMember(s1 => s1.UserName, s2 => s2.MapFrom(s3 => s3.DutUser.UserName))
                .ForMember(s1 => s1.FullName, s2 => s2.MapFrom(s3 => s3.FullName))
                .ForMember(s1 => s1.UserRolesList, s2 => s2.MapFrom(s3 => s3.DutUser.Roles.Select(p => p.RoleId)));

            CreateMap<Department, BaseFormModel>()
                .ForMember(s1 => s1.Id, s2 => s2.MapFrom(s3 => s3.Id))
                .ForMember(s1 => s1.Name, s2 => s2.MapFrom(s3 => s3.Name));
        }
    }
}