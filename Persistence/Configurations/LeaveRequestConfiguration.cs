using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.ConfigurationBase<Guid, Guid, LeaveRequest>("LeaveRequests");
            builder.Property(x => x.PositionId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.ResourceId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.ReasonId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.leaveReasonComments).HasColumnType("varchar(120)");
        }
    }
}
