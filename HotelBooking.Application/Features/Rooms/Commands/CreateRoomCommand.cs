using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Entities;
using HotelBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Rooms.Commands
{
    public class CreateRoomCommand : IRequest<int>
    {
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
    }
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, int>
    {
        IApplicationDbContext _context;
        public CreateRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomExists = await _context.Rooms
                .AnyAsync(r => r.RoomNumber == request.RoomNumber, cancellationToken);

            if (roomExists)
            {
                throw new Exception("Ye room number pehle se exist karta hai.");
            }

            var room = new Room
            {
                RoomNumber = request.RoomNumber,
                Type = request.Type,
                PricePerNight = request.PricePerNight,
                Capacity = request.Capacity,
                Description = request.Description,
                IsAvailable = true
            };

            await _context.Rooms.AddAsync(room, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return room.Id;
        }
    }
}
