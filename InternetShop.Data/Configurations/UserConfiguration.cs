using InternetShop.Domain.Entities;
using InternetShop.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace InternetShop.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(u => u.Orders)
                .WithOne(u => u.User);

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
                    .HasColumnName("Name")
                    .IsRequired(false);
            });

            builder.ComplexProperty(u => u.PhoneNumber, b =>
            {
                b.IsRequired();
                b.Property(p => p.Number)
                    .HasMaxLength(PhoneNumber.MAX_PHONENUMBER_LENGHT)
                    .HasColumnName("PhoneNumber");
            });

            builder.ComplexProperty(u => u.Credentials, b =>
            {
                b.IsRequired();

                b.Property(p => p.Login)
                    .HasMaxLength(Credentials.MAX_LOGIN_LENGHT)
                    .HasColumnName("Login");

                b.Property(p => p.PasswordHash)
                    .HasMaxLength(Credentials.MAX_PASSWORD_HASH_LENGHT)
                    .HasColumnName("PasswordHash");
            });

            builder.ComplexProperty(u => u.Role, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value)
                    .HasMaxLength(UserRole.MAX_ROLE_LENGTH)
                    .HasColumnName("Role");
            });

            builder.ComplexProperty(u => u.Email, b =>
            {
                b.IsRequired();
                b.Property(p => p.Value)
                    .HasMaxLength(Email.MAX_EMAIL_LENGHT)
                    .HasColumnName("Email");
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
