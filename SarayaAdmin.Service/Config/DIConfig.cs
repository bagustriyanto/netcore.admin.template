using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SarayaAdmin.Service.Cores;
using SarayaAdmin.Service.Services;

namespace SarayaAdmin.Service.Config {
    public class DIConfig {
        public void Initialize (IServiceCollection services) {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor> ();
            services.AddScoped<IAuthServices, AuthServices> ();
            services.AddScoped<IUserServices, UserServices> ();
            services.AddScoped<IRoleServices, RoleServices> ();
            services.AddScoped<IMenuServices, MenuServices> ();
            services.AddScoped<ITokenServices, TokenServices> ();
            services.AddScoped<IUserRoleMapServices, UserRoleMapServices> ();
            services.AddScoped<IMenuRoleMapServices, MenuRoleMapServices> ();
        }
    }
}