using Microsoft.Extensions.DependencyInjection;
using OKEX.Auto.Core.ORM.Dapper.Base;
using System;

namespace OKEX.Auto.Core.Context
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDapperDBContext<T>(this IServiceCollection services, Action<DapperDBContextOptions> setupAction) where T : DapperDBContext
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.AddScoped<DapperDBContext, T>();
            services.AddScoped<IUnitOfWorkFactory, DapperUnitOfWorkFactory>();

            return services;
        }
    }
}
