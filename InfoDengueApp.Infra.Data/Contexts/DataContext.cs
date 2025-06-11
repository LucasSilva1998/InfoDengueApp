using InfoDengueApp.Domain.Entities;
using InfoDengueApp.Infra.Data.Mappings;
using InfoDengueApp.Infra.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Contexts
{
    /// <summary>
    /// Classe de contexto para configuração do Entity Framework
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Construtor para injeção de dependência, ou seja, para que esta classe
        /// possa receber as configurações de conexão de banco de dados
        /// </summary>
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<DadosEpidemiologicos> DadosEpidemiologicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<LogAcesso> LogsAcesso { get; set; }
        public DbSet<LogInclusaoEpidemiologica> LogsInclusaoEpidemiologica { get; set; }

        /// <summary>
        /// Método para adicionar as classes de mapeamento feitas no projeto
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DadosEpidemiologicosMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new PerfilMap());
            modelBuilder.ApplyConfiguration(new LogAcessoMap());
            modelBuilder.ApplyConfiguration(new LogInclusaoEpidemiologicaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
