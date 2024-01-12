using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HR_Management.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using HR_Management.Application.Persistence.Contracts;
namespace HR_Management.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LeaveManagementDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("LeaveManagementConnectionString"));
        });

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

        return services;
    }
}