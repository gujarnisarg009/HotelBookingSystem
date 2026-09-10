using HotelBooking.Application.DTOs.Rooms;
using HotelBooking.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Rooms.Queries
{
    public class GetAllRoomQuery : IRequest<List<RoomDto>>
    {

    }
    public class GetAllRoomQueryHandler : IRequestHandler<GetAllRoomQuery,List<RoomDto>>
    {
        IApplicationDbContext _context;
        public GetAllRoomQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<RoomDto>> Handle(GetAllRoomQuery request, CancellationToken cancellationToken)
        {
            var rooms = await _context.Rooms
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
                .ToListAsync(cancellationToken);

            return rooms;
        }
    }
}
