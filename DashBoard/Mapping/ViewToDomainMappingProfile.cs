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
        }
    }
}