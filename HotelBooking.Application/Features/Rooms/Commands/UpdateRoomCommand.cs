using HotelBooking.Application.Interfaces;
using HotelBooking.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Rooms.Commands
{
    public class UpdateRoomCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public bool IsAvailable { get; set; }
    }
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, bool>
    {
        IApplicationDbContext _context;
        public UpdateRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (room == null)
            {
                throw new Exception("Room nahi mila.");
            }

            var roomExists = await _context.Rooms
                .AnyAsync(r => r.RoomNumber == request.RoomNumber && r.Id != request.Id, cancellationToken);

            if (roomExists)
            {
                throw new Exception("Ye room number dusre room ke pass already hai.");
            }

            room.RoomNumber = request.RoomNumber;
            room.Type = request.Type;
            room.PricePerNight = request.PricePerNight;
            room.Capacity = request.Capacity;
            room.Description = request.Description;
            room.IsAvailable = request.IsAvailable;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
