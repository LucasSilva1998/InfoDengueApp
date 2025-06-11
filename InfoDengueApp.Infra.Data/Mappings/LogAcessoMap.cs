using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoDengueApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoDengueApp.Infra.Mappings
{
    public class LogAcessoMap : IEntityTypeConfiguration<LogAcesso>
    {
        public void Configure(EntityTypeBuilder<LogAcesso> builder)
        {
            builder.ToTable("LogsAcesso");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Acao)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(l => l.DataHora)
                   .IsRequired();

            builder.Property(l => l.Ip)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(l => l.UsuarioId)
                   .IsRequired();

            // Relacionamento com Usuario
            builder.HasOne(l => l.Usuario)
                   .WithMany(u => u.LogsAcesso) 
                   .HasForeignKey(l => l.UsuarioId);
        }
    }
}
