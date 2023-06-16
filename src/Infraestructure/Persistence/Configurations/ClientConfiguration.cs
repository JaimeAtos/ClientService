using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Name).HasColumnType("varchar(120)").IsRequired();
            builder.Property(c => c.CountPositions).HasColumnType("int").IsRequired();
            builder.Property(c => c.LocationName).HasColumnType("varchar(150)").IsRequired();
        }
    }
}
