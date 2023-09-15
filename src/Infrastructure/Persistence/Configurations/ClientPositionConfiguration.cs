using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientPositionConfiguration : IEntityTypeConfiguration<ClientPosition>
    {
        public void Configure(EntityTypeBuilder<ClientPosition> builder)
        {
            builder.Property(x => x.PositionDescription).HasColumnType("varchar(120)").IsRequired();
            builder.Property(x => x.CurrentStateName).HasColumnType("varchar(40)");
            builder.HasOne(e => e.Client)
                .WithMany(e => e.ClientPositions)
                .HasForeignKey(e => e.ClientId)
                .IsRequired();
            
        }
    }
}