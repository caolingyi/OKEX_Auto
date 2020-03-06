using Autofac;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OKEX.Auto.Core.Caching;
using OKEX.Auto.Core.Caching.Redis;
using OKEX.Auto.Core.Context;
using OKEX.Auto.Core.ExtensionHttpClient;
using OKEX.Auto.Core.ExtensionHttpClient.Clients;
using OKEX.Auto.Core.ExtensionHttpClient.Interface;
using OKEX.Auto.Core.ORM.BaseEFRepository;
using OKEX.Auto.TradeApi.Infrastructure.Filters;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace OKEX.Auto.TradeApi.Extensions
{
    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            }).AddNewtonsoftJson();

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

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "eShopOnContainers - Ordering HTTP API",
                    Version = "v1",
                    Description = "The Ordering Service HTTP API"
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize"),
                            TokenUrl = new Uri($"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token"),
                            Scopes = new Dictionary<string, string>()
                            {
                                { "orders", "Ordering API" }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = services.BuildServiceProvider().GetService<ConnectSettings>();

            services.AddDbContext<DefaultEFDBContext>(options =>
            {
                options.UseSqlServer("");
                options.UseNpgsql("");
            });

            services.AddDapperDBContext<DefaultDapperDBContext>(options =>
            {
                options.Configuration = "";
                options.DbType = "PostgreSql";//"SqlServer"
            });

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            //Swagger
            services.AddSwaggerGen(options =>
            {
                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName,
                        new OpenApiInfo 
                        {
                            Title = $"OKEX v{description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = "多版本管理（点右上角版本切换）<br/>",
                            Contact = new OpenApiContact  { Name = "OKEX" }
                        });
                }
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
                {
                    Description = "请输入带有Bearer的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                //Json Token认证方式，此方式为全局添加

               
                // add a custom operation filter which sets default values
                //options.OperationFilter<SwaggerDefaultValues>();

                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, "Asmkt.SupplierHandle.Api.xml");
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }



        public static IServiceCollection AddApiVersion(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddVersionedApiExplorer(o => o.GroupNameFormat = "'v'VVV");

            services.AddApiVersioning(option =>
            {
                // allow a client to call you without specifying an api version
                // since we haven't configured it otherwise, the assumed api version will be 1.0
                option.AssumeDefaultVersionWhenUnspecified = true;
                option.ReportApiVersions = false;
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

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.ConfigureStartupConfig<ConnectSettings>(configuration.GetSection("ConnectSettings"));
            services.ConfigureStartupConfig<OKEXSettings>(configuration.GetSection("OKEXSettings"));
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
