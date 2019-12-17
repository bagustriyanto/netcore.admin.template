using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IRoleServices {
        BaseResponse<Role> Create (Role model);
        BaseResponse<Role> Update (Role model);
        BaseResponse<Role> Delete (long id);
        BaseResponse<Role> Get (long id);
        BaseResponse<Role> GetAll (string name, int limit, int index);
        BaseResponse<Role> DeleteList (long[] id);
    }
}