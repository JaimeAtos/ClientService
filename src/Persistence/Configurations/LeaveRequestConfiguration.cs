using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
{
    public void Configure(EntityTypeBuilder<LeaveRequest> builder)
    {
        builder.ConfigurationBase<Guid, Guid, LeaveRequest>("LeaveRequests");
        builder.Property(x => x.LeaveReasonComments).HasColumnType("varchar(500)");
    }
}