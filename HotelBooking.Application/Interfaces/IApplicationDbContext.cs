using HotelBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Room> Rooms { get; }
        DbSet<Booking> Bookings { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
