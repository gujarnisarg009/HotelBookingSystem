using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.DTOs.Bookings
{
    public class CreateBookingDto
    {
        public int RoomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
