using EQ.BLL.Abstractions;
using EQ.BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EQ.BLL
{
    public static class DIBootstrapper
    {
        public static IServiceCollection InitBll(this IServiceCollection services)
        {
            services.AddTransient<IOperatorService, OperatorService>();
            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IWindowsService, WindowService>();

            return services;
        }
    }
}
