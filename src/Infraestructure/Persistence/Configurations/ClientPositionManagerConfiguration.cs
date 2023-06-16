using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientPositionManagerConfiguration : IEntityTypeConfiguration<ClientPositionManager>
    {
        public void Configure(EntityTypeBuilder<ClientPositionManager> builder)
        {
            builder.Property(x => x.Resource).HasColumnType("varchar(80)").IsRequired();
            
            builder.HasOne(e => e.ClientPosition)
                .WithMany(e => e.ClientPositionManager)
                .HasForeignKey(e => e.ClientPositionId)
                .IsRequired();
        }
    }
}
