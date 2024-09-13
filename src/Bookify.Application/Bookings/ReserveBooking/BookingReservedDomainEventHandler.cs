
using Bookify.Application.Abstractions.Email;
using Bookify.Domain.Bookings;
using Bookify.Domain.Bookings.Events;
using Bookify.Domain.Users;
using MediatR;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class BookingReservedDomainEventHandler : INotificationHandler<BookingReservedDomainEvent>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IEmailService _emailService;
    private readonly IUserRepository _userRepository;

    public BookingReservedDomainEventHandler(
        IBookingRepository bookingRepository,
        IEmailService emailService, 
        IUserRepository userRepository)
    {
        _bookingRepository = bookingRepository;
        _emailService = emailService;
        _userRepository = userRepository;
    }

    public async Task Handle(BookingReservedDomainEvent notification, CancellationToken cancellationToken)
    {
       var booking = await _bookingRepository.GetByIdAsync(notification.BookingId, cancellationToken);  
       
        if(booking == null) 
        {
            //TODO - RETURN RESULTT
            return;
        }

        var user = await _userRepository.GetByIdAsync(booking.UserId,cancellationToken);

        if(user == null) 
        {
            //TODO - RETURN RESULTT
            return;
        }

        await _emailService.SendAsync(user.Email, "Booking Reserved",
             "You need to confirm this booking");
    }
}
