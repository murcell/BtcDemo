using AutoMapper;
using BtcDemo.Core.DTOs;
using BtcDemo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BtcDemo.Service.AutoMapper.Profiles;

public class AppUserProfile : Profile
{
    public AppUserProfile()
    {
		CreateMap<AppUserDto, AppUser>().ReverseMap();
		CreateMap<AppUser, AppUserDto>().ReverseMap();
	}
}
