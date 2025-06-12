using InfoDengueApp.Domain.Interfaces.Core;
using InfoDengueApp.Infra.Contexts;
using InfoDengueApp.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.Extensions
{
    public static class EntityFrameworkExtension
    {
        /// <summary>
        /// Método de extensão para registrar o Entity Framework no serviço de injeção de dependência.
        /// </summary>
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            // Adiciona o DataContext com a string de conexão do banco de dados
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("InfoDengue")));

            // Adicionar injeção de dependência para o UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
