using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VA.Identity.Application.Common.Interfaces;
using VA.Identity.Infrastructure.Services;

namespace VA.Identity.Infrastructure
{
    public static class Abstractions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IDomainEventService, DomainEventService>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services.AddSendGrid(configuration);
        }

        private static IServiceCollection AddSendGrid(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.Configure<SendGridEmailSenderOptions>(options =>
            {
                options.ApiKey = configuration["ExternalProviders:SendGrid:ApiKey"];
                options.SenderEmail = configuration["ExternalProviders:SendGrid:SenderEmail"];
                options.SenderName = configuration["ExternalProviders:SendGrid:SenderName"];
            });

            return services;
        }
    }
}