using Atos.Core.Commons;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientPosition> ClientsPosition { get; set; }
        public DbSet<ClientPositionManager> ClientPositionManager { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<EntityBaseAuditable<Guid, Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Id = Guid.NewGuid();
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        entry.Entity.State = true;
                        break;
                    case EntityState.Modified:
                        entry.Entity.DateLastModify = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
        }
    }
}