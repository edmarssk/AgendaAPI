using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.AppConfig
{
    public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;
        public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            this._apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfo(description));
            }
        }

        private static OpenApiInfo CreateInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Agenda Cop Api",
                Description = "Esta API é para agenda online de procedimentos odontológicos",
                TermsOfService = new Uri("https://opensource.org/licenses/MIT"),
                Contact = new OpenApiContact
                {
                    Name = "Edmar Santos",
                    Email = "edmar.santos1986@gmail.com"
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://opensource.org/licenses/MIT"),
                }
            };



            if (description.IsDeprecated)
                info.Description += " - Esta versão está obsoleta";

            return info;
        }
    }

    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {


            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerDefaultValues>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Para autenticar na API use o seguinte o seu Token assim: Bearer {My Token}",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            Scheme = "oauth2",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                         },
                         new string[] {}
                     }
                });
            });

            return services;
        }

        public static IApplicationBuilder SwaggerConfigurationApi(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var item in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{item.GroupName}/swagger.json", item.GroupName.ToUpperInvariant());

                }
            });

            return app;
        }
    }

    public class SwaggerDefaultValues: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;

            operation.Deprecated = apiDescription.IsDeprecated();

            if (operation.Parameters == null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(c => c.Name == parameter.Name);

                if(parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                parameter.Required |= description.IsRequired;
            }
        }
    }
}
