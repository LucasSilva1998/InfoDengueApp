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
    public class LogInclusaoEpidemiologicaMap : IEntityTypeConfiguration<LogInclusaoEpidemiologica>
    {
        public void Configure(EntityTypeBuilder<LogInclusaoEpidemiologica> builder)
        {
            builder.ToTable("LogsInclusaoEpidemiologica");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.DataHoraInclusao)
                   .IsRequired();

            builder.Property(l => l.UsuarioId)
                   .IsRequired();

            builder.Property(l => l.DadoEpidemiologicoId)
                   .IsRequired();

            // Relacionamento com Usuario
            builder.HasOne(l => l.Usuario)
                   .WithMany(u => u.LogsInclusaoEpidemiologica) 
                   .HasForeignKey(l => l.UsuarioId);

            // Relacionamento com DadosEpidemiologicos
            builder.HasOne(l => l.DadosEpidemiologicos)
                   .WithMany() 
                   .HasForeignKey(l => l.DadoEpidemiologicoId);
        }
    }
}
