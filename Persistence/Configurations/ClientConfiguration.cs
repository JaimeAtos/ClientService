using Atos.EFCore.Extensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ConfigurationBase<Guid, Guid, Client>("Clients");
            builder.Property(c => c.Id).HasDefaultValue("NEWID()");
            builder.Property(c => c.Name).HasColumnType("varchar(120)");
            builder.Property(c => c.CountPositions).HasColumnType("int");
            builder.Property(c => c.LocationName).HasColumnType("varchar(150)");
        
           
        }
    }
}
