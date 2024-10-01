using Bookify.Application.Abstractions.Clock;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Exceptions;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Apartments;
using Bookify.Domain.Bookings;
using Bookify.Domain.Users;

namespace Bookify.Application.Bookings.ReserveBooking;

internal sealed class ReserveBookingCommandHandler : ICommandHandler<ReserveBookingCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    public readonly  IApartmentRepository _apartmentRepository;
    public readonly  IBookingRepository _bookingRepository;
    public readonly  IUnitOfWork _unitOfWork;
    public readonly  PricingService _pricingService;
    public readonly  IDateTimeProvider _dateTimeProvider;

    public ReserveBookingCommandHandler(IUserRepository userRepository,
                                       IApartmentRepository apartmentRepository,
                                       IBookingRepository bookingRepository,
                                       IUnitOfWork unitOfWork,
                                       PricingService pricingService,
                                       IDateTimeProvider dateTimeProvider)
    {
        _userRepository = userRepository;
        _apartmentRepository = apartmentRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if(user == null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound);
        }

        var apartament = await _apartmentRepository.GetByIdAsync(request.ApartamentId, cancellationToken);   

        var duration = DateRange.Create(request.StartDate, request.EndDate);

        if (await _bookingRepository.IsOverLappingAync(apartament, duration, cancellationToken))
        {
            return Result.Failure<Guid>(BookingErrors.OverLap);
        }

        try
        {
            var booking = Booking.Reserve(apartament,
                                            user.Id,
                                            duration,
                                            _dateTimeProvider.UtcNow,
                                            _pricingService);

            _bookingRepository.Add(booking);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
        catch (ConcurrencyException)
        {

            return Result.Failure<Guid>(BookingErrors.OverLap);

        }

    }
}
