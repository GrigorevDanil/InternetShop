using InternetShop.Application.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Infrastructure.Configurations.Read
{

    public class CategoryDTOConfiguration : IEntityTypeConfiguration<CategoryDTO>
    {
        public void Configure(EntityTypeBuilder<CategoryDTO> builder)
        {
            builder.HasKey(c => c.Id);
        }
    }
}
