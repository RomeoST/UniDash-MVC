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
                .ForMember(s1 => s1.Login, s2 => s2.MapFrom(s3 => s3.DutUser.UserName))
                .ForMember(s1 => s1.Email, s2 => s2.MapFrom(s3 => s3.DutUser.Email))
                .ForMember(s1 => s1.Phone, s2 => s2.MapFrom(s3 => s3.DutUser.PhoneNumber));

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

            CreateMap<Applicant, BaseFormModel>()
                .ForMember(s1 => s1.Id, s2 => s2.MapFrom(s3 => s3.ApplicantId))
                .ForMember(s1 => s1.Name, s2 => s2.MapFrom(s3 => s3.NameApplicant));

            CreateMap<Applicant, ApplicantFormModel>()
                .ForMember(s1 => s1.ApplicantId, s2 => s2.MapFrom(s3 => s3.ApplicantId))
                .ForMember(s1 => s1.NameFound, s2 => s2.MapFrom(s3 => s3.NameFound))
                .ForMember(s1 => s1.NameApplicant, s2 => s2.MapFrom(s3 => s3.NameApplicant))
                .ForMember(s1 => s1.MailApplicant, s2 => s2.MapFrom(s3 => s3.MailApplicant))
                .ForMember(s1 => s1.PhoneApplicant, s2 => s2.MapFrom(s3 => s3.PhoneApplicant))
                .ForMember(s1 => s1.SchoolCollege, s2 => s2.MapFrom(s3 => s3.SchoolCollege))
                .ForMember(s1 => s1.Address, s2 => s2.MapFrom(s3 => s3.Address))
                .ForMember(s1 => s1.Speciality, s2 => s2.MapFrom(s3 => s3.Speciality))
                .ForMember(s1 => s1.DateEdit, s2 => s2.MapFrom(s3 => s3.DateEdit))
                .ForMember(s1 => s1.DateAdd, s2 => s2.MapFrom(s3 => s3.DateAdd))
                .ForMember(s1 => s1.MarkResult, s2 => s2.MapFrom(s3 => s3.MarkResult));
        }
    }
}