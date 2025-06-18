using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NuciDAL.Repositories;

using ProfiBotServer.Configuration;
using ProfiBotServer.DataAccess;
using ProfiBotServer.DataAccess.DataObjects;
using ProfiBotServer.Service;

namespace ProfiBotServer
{
    public static class ServiceCollectionExtensions
    {
        static DataStoreSettings dataStoreSettings;

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();

            configuration.Bind(nameof(DataStoreSettings), dataStoreSettings);

            services.AddSingleton(dataStoreSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<IRepository<PrizeEntity>>(x => new JsonRepository<PrizeEntity>(dataStoreSettings.PrizeStorePath))
            .AddSingleton<IRepository<UserEntity>>(x => new JsonRepository<UserEntity>(dataStoreSettings.UserStorePath))
            .AddSingleton<IPrizeService, PrizeService>()
            .AddAutoMapper(typeof(DataAccessMappingProfile))
            .AddAutoMapper(typeof(ServiceMappingProfile));
    }
}
