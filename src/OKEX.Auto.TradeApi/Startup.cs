using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OKEX.Auto.Core.Application;
using OKEX.Auto.Core.Domain.Interface.OKEX;
using OKEX.Auto.Core.Domain.Manager.OKEX;
using OKEX.Auto.TradeApi.Extensions;

namespace OKEX.Auto.TradeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration);
            services.AddCustomApiVersion(Configuration);
            services.AddCustomSwagger(Configuration);
            services.AddCustomConfiguration(Configuration);
            services.AddCustomDbContext(Configuration);
            services.AddCustomCache(Configuration);
            services.AddHttpClientServices(Configuration);
            services.AddRepository(Configuration);
            services.AddApplicationService();
            //services.AddCustomAuthentication(Configuration);

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new RepositoryModule());


            //services.AddScoped<IOkexFutureManager, OkexFutureManager>();

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
