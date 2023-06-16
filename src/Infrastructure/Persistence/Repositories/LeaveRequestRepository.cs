using Ardalis.Specification.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Context;

namespace Persistence.Repositories;

public class LeaveRequestRepository : RepositoryBase<LeaveRequest>, ILeaveRequestRepository
{
    private readonly ClientContext _dbContext;

    public LeaveRequestRepository(ClientContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

}