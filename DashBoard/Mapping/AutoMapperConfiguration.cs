using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using AutoMapper;
using DashBoard.Model.Models;
using DashBoard.Models;

namespace DashBoard.Mapping
{
    public static class AutoMapperConfiguration 
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewMappingProfile>();
                x.AddProfile<ViewToDomainMappingProfile>();
            });
        }
    }
}