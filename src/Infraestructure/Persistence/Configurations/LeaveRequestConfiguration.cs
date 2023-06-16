using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.Property(x => x.LeaveReasonComments).HasColumnType("varchar(500)");
        
        builder.HasOne(e => e.ClientPosition)
            .WithMany(e => e.LeaveRequests)
            .HasForeignKey(e => e.ClientPositionId)
            .IsRequired();
    }
}