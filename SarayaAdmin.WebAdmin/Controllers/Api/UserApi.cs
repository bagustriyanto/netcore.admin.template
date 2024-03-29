using System;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Server.HttpSys;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Cores;
using SarayaAdmin.WebAdmin.Config;
using SarayaAdmin.WebAdmin.Models;

namespace SarayaAdmin.WebAdmin.Controllers.Api {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route ("api/user")]
    public class UserApi : Controller {
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly IHtmlLocalizer<UserApi> _localizer;
        public UserApi (IMapper mapper, IUserServices userServices, IHtmlLocalizer<UserApi> localizer) {
            _mapper = mapper;
            _userServices = userServices;
            _localizer = localizer;
        }

        [HttpPost]
        public IActionResult Create (UserViewModel model) {
            var userModel = _mapper.Map<User> (model);
            var result = _userServices.Create (userModel);
            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpPut ("{id}")]
        public IActionResult Update (UserViewModel model) {
            var userModel = _mapper.Map<User> (model);
            var result = _userServices.Update (userModel);

            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpDelete]
        public IActionResult Delete (UserViewModel model) {
            var userModel = _mapper.Map<User> (model);
            var result = _userServices.Update (userModel);

            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }

        [HttpGet]
        public IActionResult Get (string term, int limit = 10, int index = 0) {
            var result = _userServices.GetAll (term, limit, index);
            result.Message = _localizer[result.Message].Value;

            return Json (result);
        }
    }
}