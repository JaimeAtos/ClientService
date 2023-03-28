using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repository
{
    public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly ClientContext _context;
        public MyRepositoryAsync(ClientContext context) : base(context)
        {
            _context = context;
        }

    }
}
