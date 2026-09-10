using HotelBooking.Application.DTOs.Bookings;
using HotelBooking.Application.DTOs.Rooms;
using HotelBooking.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Bookings.Queries
{
    public class GetMyBookingQuery : IRequest<List<BookingDto>>
    {
        public int UserId { get; set; }
        public GetMyBookingQuery(int userId)
        {
            UserId = userId;
        }
    }
    public class GetMyBookingQueryHandler : IRequestHandler<GetMyBookingQuery ,List<BookingDto>>
    {
        IApplicationDbContext _context;
        public GetMyBookingQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<BookingDto>> Handle(GetMyBookingQuery request, CancellationToken cancellationToken)
        {
            var myBookings = await _context.Bookings
                .Where(b => b.UserId == request.UserId)
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

            return myBookings;
        }
    }
}
