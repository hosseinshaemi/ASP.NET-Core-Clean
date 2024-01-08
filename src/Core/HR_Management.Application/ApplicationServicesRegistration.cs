using System.Reflection;
using HR_Management.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;
namespace HR_Management.Application;

public static class ApplicationServicesRegistration
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}