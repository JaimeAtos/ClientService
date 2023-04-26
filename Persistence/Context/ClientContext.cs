using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ClientContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        public ClientContext(DbContextOptions options, IDateTimeService dateTime ) : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientPosition> ClientsPositions { get; set; }
        public DbSet<ClientPositionManager> ClientPositionManagers { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellation = new CancellationToken())
        {
            //TODO: agregar despues la AuditableEntityBase
            foreach (var entry in base.ChangeTracker.Entries<EntityBaseAuditable<Guid, Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = _dateTime.NowUtc;
                        entry.Entity.State = true;
                        break;
                    //case EntityState.Modified:                
                    //break;               
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellation);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientPosition>()
            .HasOne<ClientPositionManager>()
            .WithOne()
            .HasForeignKey<ClientPositionManager>(e => e.ClientPositionId)
            .IsRequired();
        }
    }
}
