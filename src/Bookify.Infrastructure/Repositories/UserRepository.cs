
using Bookify.Domain.Users;
using Bookify.Infrastructure.Database;

namespace Bookify.Infrastructure.Repositories;

internal sealed class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext Dbcontext) : base(Dbcontext)
    {
    }
}
