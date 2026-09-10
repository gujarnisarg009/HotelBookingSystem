using HotelBooking.Domain.Common;
using HotelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Entities
{
    public class Booking : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalAmount { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }
}
