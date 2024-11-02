using InternetShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InternetShop.Domain.ValueObjects;

namespace InternetShop.Infrastructure.Configurations.Write
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.HasKey(u => u.Id);

            builder.ComplexProperty(u => u.FullName, b =>
            {
                b.IsRequired();

                b.Property(p => p.Surname)
                    .HasMaxLength(FullName.MAX_SURNAME_LENGHT)
                    .HasColumnName("Surname");

                b.Property(p => p.Name)
                    .HasMaxLength(FullName.MAX_NAME_LENGHT)
                    .HasColumnName("Name");

                b.Property(p => p.Lastname)
                    .HasMaxLength(FullName.MAX_LASTNAME_LENGHT)
                    .HasColumnName("Lastname")
                    .IsRequired(false);
            });

            builder.ComplexProperty(u => u.Gender, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value)
                    .HasMaxLength(Gender.MAX_GENDER_LENGHT)
                    .HasColumnName("Gender");
            });

        }
    }
}
