

using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal sealed class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext Dbcontext) : base(Dbcontext)
    {
    }

    private readonly BookingStatus[] ActiveBookingStatususes =
    {
        BookingStatus.Reserved,
        BookingStatus.Confirmed,
        BookingStatus.Completed    
    };

    public async Task<bool> IsOverLappingAync(
        Apartment apartment,
        DateRange dateRange, 
        CancellationToken cancellationToken)
    {
        return await DbContext
             .Set<Booking>()
             .AnyAsync(
              booking =>
              booking.ApartmentId == apartment.Id &&
              booking.Duration.Start <= dateRange.End &&
              booking.Duration.End >= dateRange.Start &&
              ActiveBookingStatususes.Contains(booking.Status),
              cancellationToken);
    }
}
