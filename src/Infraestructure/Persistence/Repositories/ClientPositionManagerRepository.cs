using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories;

public class ClientPositionManagerRepository : RepositoryBase<ClientPositionManager>, IClientPositionManagerRepository
{
    private readonly ClientContext _dbContext;
    
    public ClientPositionManagerRepository(ClientContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}