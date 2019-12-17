using System.Collections.Generic;
using AutoMapper;
using SarayaAdmin.Entity.Custom;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.WebAdmin.Models;

namespace SarayaAdmin.WebAdmin.Config {
    public class MapperProfile : Profile {
        public MapperProfile () {
            CreateMap<CredentialViewModel, Credentials> ();
            CreateMap<UserViewModel, User> ()
                .ForMember (dest => dest.CredentialId, opt => opt.MapFrom (src => src.Credential));
            CreateMap<MenuViewModel, Menu> ();
            CreateMap<RoleViewModel, Role> ();
            CreateMap<RoleMapViewModel, RoleMap> ();
            CreateMap<MenuMapViewModel, CustomMenuMap> ()
                .ForMember (dest => dest.MenuRoleMap, opt => opt.MapFrom (src => src.MenuRoleMap));

        }
    }
}