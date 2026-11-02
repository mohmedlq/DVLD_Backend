using DVLD.Application.Interfaces;
using DVLD.Application.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DVLD.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            // Services
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IApplicationTypesService, ApplicationTypesService>();
            services.AddScoped<ILicenseClassesService, LicenseClassesService>();
            services.AddScoped<IDriversService, DriversService>();
            services.AddScoped<ITestTypesService, TestTypesService>();
            services.AddScoped<IApplicationsService, ApplicationsService>();
            services.AddScoped<ILocalDrivingLicenseApplicationsService, LocalDrivingLicenseApplicationsService>();
            services.AddScoped<ITestAppointmentsService, TestAppointmentsService>();
            services.AddScoped<ITestsService, TestsService>();
            services.AddScoped<ILicensesService, LicensesService>();
            services.AddScoped<IInternationalLicensesService, InternationalLicensesService>();
            services.AddScoped<IDetainedLicensesService, DetainedLicensesService>();
            services.AddScoped<ITasksService, TasksService>();
            services.AddScoped<IMassagesService, MassagesService>();
            services.AddScoped<ILogsService, LogsService>();
            services.AddScoped<IDeletedLicensesService, DeletedLicensesService>();
            services.AddScoped<IOldUserNamesLogsService, OldUserNamesLogsService>();

            return services;
        }
    }
}
