using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Ordering.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Mail;
using Ordering.Application.Models;
using Ordering.Application.Contracts.Infrastracture;

namespace Ordering.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<OrderContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));



            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }




    }
}
