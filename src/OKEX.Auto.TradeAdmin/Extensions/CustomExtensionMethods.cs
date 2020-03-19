using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OKEX.Auto.Core.Caching;
using OKEX.Auto.Core.Caching.Redis;
using OKEX.Auto.Core.Context;
using OKEX.Auto.Core.ExtensionHttpClient;
using OKEX.Auto.Core.ExtensionHttpClient.Clients;
using OKEX.Auto.Core.ExtensionHttpClient.Interface;
using OKEX.Auto.Core.ORM;
using OKEX.Auto.Core.ORM.BaseEFRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OKEX.Auto.TradeAdmin.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc()
       .AddJsonOptions(options =>
       {

       }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.ConfigureStartupConfig<ConnectSettings>(configuration.GetSection("ConnectSettings"));
            services.ConfigureStartupConfig<OKEXSettings>(configuration.GetSection("OKEXSettings"));
            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.BuildServiceProvider().GetService<ConnectSettings>();

            services.AddDbContext<DefaultEFDBContext>(options =>
            {
                //options.UseSqlServer(settings.ConnectionString);
                options.UseNpgsql(settings.ConnectionString);
            });

            services.AddDapperDBContext<DefaultDapperDBContext>(options =>
            {
                options.Configuration = settings.ConnectionString;
                options.DbType = "PostgreSql";//"SqlServer"
            });

            return services;
        }

        public static IServiceCollection AddCustomCache(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.BuildServiceProvider().GetService<ConnectSettings>();
            // Redis
            services.AddSingleton<ICacheManager>(provider => new RedisCacheManager(new RedisConnectionWrapper(settings.RedisConnectionString, settings.RedisDb)));

            return services;
        }

        public static IServiceCollection AddHttpClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient<IOKEXHttpClient, OKEXHttpClient>();

            return services;
        }

        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            const string servicesEndsWith = "Repository";
            var localServices = typeof(EFRepository<>).Assembly.GetTypes().Where(t => t.Name.EndsWith(servicesEndsWith) && t.GetInterfaces().Any(i => i.Name.EndsWith(servicesEndsWith))).ToList();
            if (localServices.Count > 0)
            {
                foreach (var (item, i) in from item in localServices from i in item.GetInterfaces() select (item, i))
                {
                    services.AddTransient(i, item);
                }
            }

            return services;
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //create instance of config
            var config = new TConfig();

            //bind it to the appropriate section of configuration
            configuration.Bind(config);

            //and register it as a service
            services.AddSingleton(config);

            return config;
        }
    }

    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(EFRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
