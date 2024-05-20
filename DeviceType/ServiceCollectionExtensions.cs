using Microsoft.Extensions.DependencyInjection;

namespace DeviceType
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBlazorCurrentDevice(this IServiceCollection services)
        {
            return services.AddTransient<IBlazorCurrentDeviceService, BlazorCurrentDeviceService>();
        }
    }
}
