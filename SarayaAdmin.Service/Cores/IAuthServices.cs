using System.Threading.Tasks;
using SarayaAdmin.Entity.Model;
using SarayaAdmin.Service.Dto;
using SarayaAdmin.Service.Responses;

namespace SarayaAdmin.Service.Cores {
    public interface IAuthServices {
        Task<BaseResponse<Credentials>> Login (Credentials model);
        BaseResponse<Credentials> Register (Credentials model);
        BaseResponse<Credentials> ResetPassword (DtoCredential model);
        BaseResponse<Credentials> ForgotPassword (Credentials model);
        BaseResponse<Credentials> NewPassword (Credentials model);
        BaseResponse<Credentials> Verification (Credentials model);
    }
}