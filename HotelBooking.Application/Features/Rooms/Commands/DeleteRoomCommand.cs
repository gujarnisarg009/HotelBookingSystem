using HotelBooking.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Features.Rooms.Commands
{
    public class DeleteRoomCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteRoomCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand,bool>
    {
        IApplicationDbContext _context;
        public DeleteRoomCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _context.Rooms
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (room == null)
            {
                throw new Exception("Room nahi mila.");
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
