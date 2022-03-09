using AgendaCOP.API.Extensions;
using AgendaCOP.Business.Interfaces.Repository;
using AgendaCOP.Business.Interfaces.Services;
using AgendaCOP.Business.Interfaces.Util;
using AgendaCOP.Business.Notificacoes;
using AgendaCOP.Business.Services;
using AgendaCOP.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCOP.API.AppConfig
{
    public static class DependencyInjectConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProcedimentoRepository, ProcedimentoRepository>();
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IClienteServices, ClienteServices>();
            services.AddScoped<IAgendaServices, AgendaServices>();
            services.AddScoped<IProcedimentoServices, ProcedimentoServices>();

            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUser, AspNetUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigOptions>();

            return services;
        }
    }
}
