using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NKS.Accounts.Api.Handlers;
using NKS.Accounts.Api.Mappers;
using NKS.Accounts.Core.Interfaces;
using NKS.Accounts.Core.Services;
using Serilog;


namespace NKS.Accounts.Api.Configuration
{
    public static class Dependencies
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var swaggerConfig = config.GetSection("SwaggerConfiguration").Get<Swagger>();

            Log.Information("Configuring services.");
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
            services.AddTransient<IAccountRequestMapper, AccountRequestMapper>();
            services.AddTransient<IPhoneNumberMapper, PhoneNumberMapper>();
            services.AddMvcCore()
                .AddMvcOptions(options =>
                {
                    options.AllowEmptyInputInBodyModelBinding = true;
                });

            services.Configure<ApiBehaviorOptions>(options =>
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext
                    .ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new
                    {
                        Name = e.Key,
                        Message = e.Value.Errors.FirstOrDefault().ErrorMessage
                    }).ToArray();
                return new BadRequestObjectResult(errors);
            });

            
            services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                options.IncludeXmlComments(xmlCommentsPath);
                options.SwaggerDoc($"v{swaggerConfig.Version}", new OpenApiInfo
                {
                    Title = swaggerConfig.Title,
                    Version = $"v{swaggerConfig.Version}",
                    Description = swaggerConfig.Description
                });
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
                options.EnableAnnotations();
                options.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Basic Authorization header using the Bearer scheme.",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new List<string>()
                    }
                });
            });
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = ApiVersion.Default;
                options.ApiVersionReader = ApiVersionReader.Combine
                (
                    new MediaTypeApiVersionReader("version"),
                    new MediaTypeApiVersionReader("x-version"));
                options.ReportApiVersions = true;
            });
            return services;
        }
    }
}
