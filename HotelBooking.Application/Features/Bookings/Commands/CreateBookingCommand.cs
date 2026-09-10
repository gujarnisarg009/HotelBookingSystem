using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<int>
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, int>
    {
        IApplicationDbContext context;
        public CreateBookingCommandHandler(IApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.CheckOutDate <= request.CheckInDate)
            {
                throw new Exception("Check-out date Check-in ke baad honi chahiye.");
            }

            var room = await context.Rooms
                .FirstOrDefaultAsync(r => r.Id == request.RoomId, cancellationToken);

            if (room == null)
            {
                throw new Exception("Room nahi mila.");
            }

            var isBooked = await this.context.Bookings.AnyAsync(b =>
                b.RoomId == request.RoomId &&
                b.Status != BookingStatus.Cancelled &&
                request.CheckInDate < b.CheckOutDate &&
                request.CheckOutDate > b.CheckInDate,
                cancellationToken);

            if (isBooked)
            {
                throw new Exception("Room in dates par already booked hai.");
            }

            int days = (request.CheckOutDate - request.CheckInDate).Days;
            decimal totalAmount = days * room.PricePerNight;

            var booking = new Booking
            {
                RoomId = request.RoomId,
                UserId = request.UserId,
                CheckInDate = request.CheckInDate,
                CheckOutDate = request.CheckOutDate,
                TotalAmount = totalAmount,
                Status = BookingStatus.Confirmed
            };

            await this.context.Bookings.AddAsync(booking, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);

            return booking.Id;
        }
    }
}
