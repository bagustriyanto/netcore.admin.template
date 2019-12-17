using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IUserRoleMapServices {
        BaseResponse<RoleMap> Create (RoleMap model);
        BaseResponse<RoleMap> Update (RoleMap model);
        BaseResponse<RoleMap> Delete (long id);
        BaseResponse<RoleMap> Get (long id);
        BaseResponse<RoleMap> GetAll (string searchTerm, int limit, int index);
    }
}