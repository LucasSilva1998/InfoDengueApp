using InfoDengueApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoDengueApp.Infra.Mapping
{
    public class PerfilMap : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("Perfis");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(p => p.Usuarios)
                .WithOne(u => u.Perfil)
                .HasForeignKey(u => u.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
