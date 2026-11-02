using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DVLD.Infrastructure.Data;
using DVLD.Infrastructure.Repositories;
using DVLD.Domain.Interfaces;

namespace DVLD.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DvldDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IApplicationsRepository, ApplicationsRepository>();
            services.AddScoped<IApplicationTypesRepository, ApplicationTypesRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<ILicenseClassesRepository, LicenseClassesRepository>();
            services.AddScoped<IDriversRepository, DriversRepository>();
            services.AddScoped<ITestTypesRepository, TestTypesRepository>();
            services.AddScoped<ILocalDrivingLicenseApplicationsRepository, LocalDrivingLicenseApplicationsRepository>();
            services.AddScoped<ITestAppointmentsRepository, TestAppointmentsRepository>();
            services.AddScoped<ITestsRepository, TestsRepository>();
            services.AddScoped<ILicensesRepository, LicensesRepository>();
            services.AddScoped<IInternationalLicensesRepository, InternationalLicensesRepository>();
            services.AddScoped<IDetainedLicensesRepository, DetainedLicensesRepository>();
            services.AddScoped<ITasksRepository, TasksRepository>();
            services.AddScoped<IMassagesRepository, MassagesRepository>();
            services.AddScoped<ILogsRepository, LogsRepository>();
            services.AddScoped<IDeletedLicensesRepository, DeletedLicensesRepository>();
            services.AddScoped<IOldUserNamesLogsRepository, OldUserNamesLogsRepository>();

            return services;
        }
    }
}
