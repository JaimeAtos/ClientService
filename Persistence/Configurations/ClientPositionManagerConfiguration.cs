using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientPositionManagerConfiguration : IEntityTypeConfiguration<ClientPositionManager>
    {
        public void Configure(EntityTypeBuilder<ClientPositionManager> builder)
        {
            builder.ConfigurationBase<Guid, Guid, ClientPositionManager>("ClientesPositionManagers");
            builder.Property(x => x.Id).HasDefaultValue("NEWID()");
            builder.Property(x => x.ClientPositionId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.ResourceId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.Resource).HasColumnType("varchar(80)");
            
            
        }

       
    }
}
