using InfoDengueApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfoDengueApp.Infra.Data.Mappings
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Nome)
                .IsRequired()
                .HasMaxLength(150);

            builder.OwnsOne(u => u.Cpf, cpf =>
            {
                cpf.Property(c => c.Numero)
                    .HasColumnName("Cpf")
                    .IsRequired()
                    .HasMaxLength(11);
            });

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.SenhaHash)
                .IsRequired();

            builder.Property(u => u.Ativo)
                .IsRequired();

            builder.Property(u => u.RefreshToken)
                .HasMaxLength(200); // Define o tamanho máximo para o token

            builder.Property(u => u.RefreshTokenExpiracao);

            builder.HasOne(u => u.Perfil)
                .WithMany()
                .HasForeignKey(u => u.PerfilId)
                .IsRequired();
        }
    }
}
