using DVLD.Business.Interfaces;
using DVLD.Business.Services;
using DVLD.DataAccess;
using DVLD.DataAccess.Context;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DVLD.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<DvldDbContext>();
            services.AddDataAccessDependencies(configuration);
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
            //  (Services)
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}