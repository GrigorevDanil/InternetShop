using InternetShop.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InternetShop.Infrastructure.Configurations.Read
{
    public class BrandDTOConfiguration : IEntityTypeConfiguration<BrandDTO>
    {
        public void Configure(EntityTypeBuilder<BrandDTO> builder)
        {
            builder.HasKey(b => b.Id);
        }
    }
}
