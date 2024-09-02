

using Bookify.Domain.Apartments;

namespace Bookify.Domain.Bookings;

public interface  IBookingRepository
{
    Task<bool> IsOverLappingAync(Apartment apartment,DateRange dateRange,CancellationToken cancellationToken);

    void Add(Booking booking);  
}
