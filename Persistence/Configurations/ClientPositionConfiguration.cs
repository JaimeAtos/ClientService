﻿using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class ClientPositionConfiguration : IEntityTypeConfiguration<ClientPosition>
    {
        public void Configure(EntityTypeBuilder<ClientPosition> builder)
        {
            builder.ConfigurationBase<Guid, Guid, ClientPosition>("ClientsPositions");
            builder.Property(x => x.Id).HasDefaultValue("NEWID()");
            builder.Property(x => x.PositionId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.PositionDescription).HasColumnType("varchar(120)");
            builder.Property(x => x.ClientId).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.CurrentStateID).HasColumnType("UNIQUEIDENTIFIER");
            builder.Property(x => x.CurrentStateName).HasColumnType("varchar(40)");
        }
    }
}
