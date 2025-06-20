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
using ProfiBotServer.Service.Notifications;

namespace ProfiBotServer
{
    public static class ServiceCollectionExtensions
    {
        static DataStoreSettings dataStoreSettings;
        static NotificationSettings notificationSettings;
        static NuciLoggerSettings loggingSettings;

        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            dataStoreSettings = new DataStoreSettings();
            notificationSettings = new NotificationSettings();
            loggingSettings = new NuciLoggerSettings();

            configuration.Bind(nameof(DataStoreSettings), dataStoreSettings);
            configuration.Bind(nameof(NotificationSettings), notificationSettings);
            configuration.Bind(nameof(NuciLoggerSettings), loggingSettings);

            services.AddSingleton(dataStoreSettings);
            services.AddSingleton(notificationSettings.SmtpNotifierSettings);
            services.AddSingleton(loggingSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services) => services
            .AddSingleton<ILogger, NuciLogger>()
            .AddSingleton<IRepository<PrizeEntity>>(x => new JsonRepository<PrizeEntity>(dataStoreSettings.PrizeStorePath))
            .AddSingleton<IRepository<QrCodeEntity>>(x => new JsonRepository<QrCodeEntity>(dataStoreSettings.QrCodeStorePath))
            .AddSingleton<IRepository<UserEntity>>(x => new JsonRepository<UserEntity>(dataStoreSettings.UserStorePath))
            .AddSingleton<ISmtpNotifier, SmtpNotifier>()
            .AddSingleton<IPrizeService, PrizeService>()
            .AddSingleton<IQrCodeService, QrCodeService>()
            .AddAutoMapper(typeof(DataAccessMappingProfile))
            .AddAutoMapper(typeof(ServiceMappingProfile));
    }
}
