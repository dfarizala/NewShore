using MediatR;
using Microsoft.EntityFrameworkCore;
using NewshoreTest.Api.Domain.Interfaces;
using NewshoreTest.Api.Infrastructure;
using NewshoreTest.Api.Infrastructure.Repository;
using System.Reflection;

namespace NewshoreTest.Api
{
    public static class DependencyContainer
    {
        public static IServiceCollection AddNewshoreServices(this IServiceCollection services,
                                                                  IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer
                (configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
