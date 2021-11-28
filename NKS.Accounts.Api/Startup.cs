using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NKS.Accounts.Api.Configuration;
using NKS.Accounts.Core.Configuration;
using NKS.Accounts.Infrastructure.Configuration;
using Serilog;

namespace NKS.Accounts.Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Information("Configure services");
            services.Configure<Swagger>(Configuration.GetSection("SwaggerConfiguration"));

            services
                .AddApiConfiguration(Configuration)
                .AddInfrastruture(Configuration["ConnectionStrings:AccountsDatabase"])
                .AddDomainServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var swaggerConfig = Configuration.GetSection("SwaggerConfiguration").Get<Swagger>();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint($"/swagger/v{swaggerConfig.Version}/swagger.json", "NKS.Customers.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
