using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IUserServices {
        BaseResponse<User> Create (User model);
        BaseResponse<User> Update (User model);
        BaseResponse<User> Delete (User model);
        BaseResponse<User> Get (User model);
        BaseResponse<User> GetAll (string term, int limit, int index);
    }
}