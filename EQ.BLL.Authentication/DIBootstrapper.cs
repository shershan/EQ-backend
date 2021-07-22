using Microsoft.Extensions.DependencyInjection;

namespace EQ.BLL.Authentication
{
    public static class DIBootstrapper
    {
        public static IServiceCollection InitAuthenticationBLL(this IServiceCollection services)
        {
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
