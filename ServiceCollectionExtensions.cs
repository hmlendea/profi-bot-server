using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;
using NuciLog;
using NuciLog.Configuration;
using NuciLog.Core;
using ProfiBotServer.Configuration;
using ProfiBotServer.DataAccess;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Service;

namespace ProfiBotServer
{
    public static class ServiceCollectionExtensions
    {
        static DataStoreSettings dataStoreSettings;
        static NuciLoggerSettings loggingSettings;

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();
            loggingSettings = new NuciLoggerSettings();

            configuration.Bind(nameof(DataStoreSettings), dataStoreSettings);
            configuration.Bind(nameof(NuciLoggerSettings), loggingSettings);

            services.AddSingleton(dataStoreSettings);
            services.AddSingleton(loggingSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<ILogger, NuciLogger>()
            .AddSingleton<IRepository<PrizeEntity>>(x => new JsonRepository<PrizeEntity>(dataStoreSettings.PrizeStorePath))
            .AddSingleton<IRepository<UserEntity>>(x => new JsonRepository<UserEntity>(dataStoreSettings.UserStorePath))
            .AddSingleton<IPrizeService, PrizeService>()
            .AddAutoMapper(typeof(DataAccessMappingProfile))
            .AddAutoMapper(typeof(ServiceMappingProfile));
    }
}
