using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using VA.Identity.Application.Jwt;

namespace VA.Identity.Infrastructure.Security.User
{
    public static class Abstractions
    {
        public static IServiceCollection AddCurrentUserContextConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICurrentUserContext, CurrentUserContext>();

            return services;
        }
    }
}