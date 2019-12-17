namespace SarayaAdmin.Service.Cores {
    public interface ITokenServices {
        string CreateToken (string username, string roleId);
    }
}