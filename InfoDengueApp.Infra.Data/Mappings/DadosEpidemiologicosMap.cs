using InfoDengueApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoDengueApp.Infra.Data.Mappings
{
    public class DadosEpidemiologicosMap : IEntityTypeConfiguration<DadosEpidemiologicos>
    {
        public void Configure(EntityTypeBuilder<DadosEpidemiologicos> builder)
        {
            builder.ToTable("DadosEpidemiologicos");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Municipio)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.CodigoIbge)
                   .IsRequired();

            builder.Property(d => d.SemanaEpi)
                   .IsRequired();

            builder.Property(d => d.Arbovirose)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(d => d.CasosEstimados)
                   .IsRequired();

            builder.Property(d => d.DataColeta)
                   .IsRequired();
        }
    }
}
           


