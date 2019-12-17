using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using SarayaAdmin.Entity.Custom;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Cores;
using SarayaAdmin.WebAdmin.Config;
using SarayaAdmin.WebAdmin.Models;

namespace SarayaAdmin.WebAdmin.Controllers.Api {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route ("api/menu-role")]
    public class MenuRoleMapApi : Controller {
        private readonly IMapper _mapper;
        private readonly IMenuRoleMapServices _menuRoleServices;
        private readonly IHtmlLocalizer<UserApi> _localizer;
        public MenuRoleMapApi (IMapper mapper, IMenuRoleMapServices menuRoleServices, IHtmlLocalizer<UserApi> localizer) {
            _mapper = mapper;
            _menuRoleServices = menuRoleServices;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult Create (List<MenuMapViewModel> model) {
            var userRoleModel = _mapper.Map<List<CustomMenuMap>> (model);
            var result = _menuRoleServices.CreateUpdateRemove (userRoleModel);

            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpGet ("{roleId}")]
        public IActionResult Get (long roleId) {
            var result = _menuRoleServices.Get (roleId);
            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }
    }
}