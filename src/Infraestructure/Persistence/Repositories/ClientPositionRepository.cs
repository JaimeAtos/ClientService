using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories;

public class ClientPositionRepository : RepositoryBase<ClientPosition>, IClientPositionRepository
{
    private readonly ClientContext _dbContext;

    public ClientPositionRepository(ClientContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}