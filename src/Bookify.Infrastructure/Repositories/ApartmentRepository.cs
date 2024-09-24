

using Bookify.Domain.Apartments;
using Bookify.Infrastructure.Database;

namespace Bookify.Infrastructure.Repositories;

internal sealed class ApartmentRepository : Repository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(ApplicationDbContext Dbcontext) : base(Dbcontext)
    {
    }
}
