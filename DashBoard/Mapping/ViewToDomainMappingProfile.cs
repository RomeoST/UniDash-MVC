using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AutoMapper;
using DashBoard.Model.Models;
using DashBoard.Models;
using Microsoft.Owin.Security;

namespace DashBoard.Mapping
{
    public class ViewToDomainMappingProfile : Profile
    {
        public ViewToDomainMappingProfile()
        {
            CreateMap<RegisterFormModel, DutUser>()
                .ForMember( dest  => dest.ClientProfile, 
                            input => input.MapFrom(i => new ClientProfile{FullName = i.Name}));

            CreateMap<EditUserFormModel, DutUser>()
                .ForMember(dest => dest.ClientProfile,
                    input => input.MapFrom(i => new ClientProfile {FullName = i.FullName}))
                .ForMember(dest => dest.UserName, input => input.MapFrom(i => i.Login))
                .ForMember(dest => dest.PhoneNumber, input => input.MapFrom(i => i.Phone));

            CreateMap<ApplicantFormModel, Applicant>()
                .ForMember(s1 => s1.ApplicantId, s2 => s2.MapFrom(s3 => s3.ApplicantId))
                .ForMember(s1 => s1.NameFound, s2 => s2.MapFrom(s3 => s3.NameFound))
                .ForMember(s1 => s1.NameApplicant, s2 => s2.MapFrom(s3 => s3.NameApplicant))
                .ForMember(s1 => s1.MailApplicant, s2 => s2.MapFrom(s3 => s3.MailApplicant))
                .ForMember(s1 => s1.PhoneApplicant, s2 => s2.MapFrom(s3 => s3.PhoneApplicant))
                .ForMember(s1 => s1.SchoolCollege, s2 => s2.MapFrom(s3 => s3.SchoolCollege))
                .ForMember(s1 => s1.Address, s2 => s2.MapFrom(s3 => s3.Address))
                .ForMember(s1 => s1.Speciality, s2 => s2.MapFrom(s3 => s3.Speciality))
                .ForMember(s1 => s1.MarkResult, s2 => s2.MapFrom(s3 => s3.MarkResult));
        }
    }
}