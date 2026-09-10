using HotelBooking.Application.DTOs.Bookings;
using HotelBooking.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Bookings.Queries
{
    public class GetAllBookingQuery : IRequest<List<BookingDto>>
    {

    }
    public class GetAllBookingQueryHandler : IRequestHandler<GetAllBookingQuery, List<BookingDto>>
    {
        IApplicationDbContext _context;
        public GetAllBookingQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookingDto>> Handle(GetAllBookingQuery request, CancellationToken cancellationToken)
        {
            var bookings = await _context.Bookings
                .Select(b => new BookingDto
                {
                    Id = b.Id,
                    RoomId = b.RoomId,
                    UserId = b.UserId,
                    CheckInDate = b.CheckInDate,
                    CheckOutDate = b.CheckOutDate,
                    TotalAmount = b.TotalAmount,
                    Status = b.Status.ToString()
                })
                .ToListAsync(cancellationToken);

            return bookings;
        }
    }
}
