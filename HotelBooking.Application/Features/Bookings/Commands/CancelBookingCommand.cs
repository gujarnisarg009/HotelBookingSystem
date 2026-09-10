using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Bookings.Commands
{
    public class CancelBookingCommand : IRequest<bool>
    {
        public int BookingId { get; set; }
        public CancelBookingCommand(int bookingId)
        {
            BookingId = bookingId;
        }
    }
    public class CancelBookingCommandHandler : IRequestHandler<CancelBookingCommand, bool>
    {
        IApplicationDbContext _context;
        public CancelBookingCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.Id == request.BookingId, cancellationToken);

            if (booking == null)
            {
                throw new Exception("Booking nahi mili.");
            }

            if (booking.Status == BookingStatus.Cancelled)
            {
                throw new Exception("Ye booking pehle se cancelled hai.");
            }

            booking.Status = BookingStatus.Cancelled;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
