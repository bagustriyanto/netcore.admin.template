using System.Collections.Generic;
using SarayaAdmin.Entity.Custom;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IMenuRoleMapServices {
        BaseResponse<MenuRoleMap> CreateUpdateRemove (List<CustomMenuMap> model);
        BaseResponse<MenuRoleMap> Get (long roleId);
    }
}