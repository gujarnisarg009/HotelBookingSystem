using HotelBooking.Domain.Common;
using HotelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Description { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
