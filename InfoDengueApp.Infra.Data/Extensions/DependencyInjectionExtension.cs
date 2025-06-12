using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Domain.Interfaces.Repository;
using InfoDengueApp.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InfoDengueApp.Infra.Data.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositórios
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPerfilRepository, PerfilRepository>();
            services.AddScoped<IDadoEpidemiologicoRepository, DadoEpidemiologicoRepository>();
            services.AddScoped<ILogAcessoRepository, LogAcessoRepository>();
            services.AddScoped<ILogInclusaoEpidemiologicaRepository, LogInclusaoEpidemiologicaRepository>();

            return services;
        }
    }
}
