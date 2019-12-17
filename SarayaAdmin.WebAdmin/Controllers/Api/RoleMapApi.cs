using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Cores;
using SarayaAdmin.WebAdmin.Config;
using SarayaAdmin.WebAdmin.Models;

namespace SarayaAdmin.WebAdmin.Controllers.Api {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route ("api/user-role")]
    public class RoleMapApi : Controller {
        private readonly IMapper _mapper;
        private readonly IUserRoleMapServices _userRoleServices;
        private readonly IHtmlLocalizer<UserApi> _localizer;
        public RoleMapApi (IMapper mapper, IUserRoleMapServices userRoleServices, IHtmlLocalizer<UserApi> localizer) {
            _mapper = mapper;
            _userRoleServices = userRoleServices;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult Create (RoleMapViewModel model) {
            var userRoleModel = _mapper.Map<RoleMap> (model);
            var result = _userRoleServices.Create (userRoleModel);
            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpPut ("{id}")]
        public IActionResult Update (RoleMapViewModel model) {
            var userRoleModel = _mapper.Map<RoleMap> (model);
            var result = _userRoleServices.Update (userRoleModel);

            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpDelete]
        public IActionResult Delete (long id) {
            if (id == 0)
                return NotFound ();

            var result = _userRoleServices.Delete (id);

            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpGet]
        public IActionResult Get (string term, int limit = 10, int index = 0) {
            var result = _userRoleServices.GetAll (term, limit, index);
            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }
    }
}