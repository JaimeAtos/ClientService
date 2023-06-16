using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories;

public class ClientRepository : RepositoryBase<Client>, IClientRepository
{
    private readonly ClientContext _dbContext;

    public ClientRepository(ClientContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}