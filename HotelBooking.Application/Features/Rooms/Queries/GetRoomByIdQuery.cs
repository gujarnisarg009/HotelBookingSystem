using HotelBooking.Application.DTOs.Rooms;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Application.Interfaces;

namespace HotelBooking.Application.Features.Rooms.Queries
{
    public class GetRoomByIdQuery : IRequest<RoomDto>
    {
        public int Id { get; set; }
        public GetRoomByIdQuery(int id)
        {
            Id = id;
        }
    }
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomByIdQuery,RoomDto>
    {
        IApplicationDbContext _context;
        public GetRoomByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<RoomDto?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .Where(r => r.Id == request.Id)
                .Select(r => new RoomDto
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Type = r.Type.ToString(),
                    PricePerNight = r.PricePerNight,
                    Capacity = r.Capacity,
                    Description = r.Description,
                    IsAvailable = r.IsAvailable
                })
                .FirstOrDefaultAsync(cancellationToken);

            return room;
        }
    }
}
