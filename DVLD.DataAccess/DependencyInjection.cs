using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DVLD.DataAccess.Context;
using DVLD.DataAccess.Repositories;
using DVLD.Domain.Interfaces;

namespace DVLD.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DvldDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}