using Bookify.Application.Abstractions.Messaging;
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

    public ReserveBookingCommandHandler(IUserRepository userRepository,
                                       IApartmentRepository apartmentRepository,
                                       IBookingRepository bookingRepository, 
                                       IUnitOfWork unitOfWork, 
                                       PricingService pricingService)
    {
        _userRepository = userRepository;
        _apartmentRepository = apartmentRepository;
        _bookingRepository = bookingRepository;
        _unitOfWork = unitOfWork;
        _pricingService = pricingService;
    }

    public async Task<Result<Guid>> Handle(ReserveBookingCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
