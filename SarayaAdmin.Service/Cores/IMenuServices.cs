using System.Collections.Generic;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IMenuServices {
        BaseResponse<Menu> Create (Menu model);
        BaseResponse<Menu> Update (Menu model);
        BaseResponse<Menu> Delete (long id);
        BaseResponse<Menu> Get (Menu model);
        BaseResponse<Menu> GetList (string title, int index, int limit);
        BaseResponse<Menu> DeleteList (long[] id);
    }
}