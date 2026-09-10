using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.DTOs.Rooms
{
    public class RoomDto
    {
        //Dashbord Pe Show
        public int Id { get; set; }
        public string RoomNumber { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Enum ko string me dikhayenge jaise "Deluxe"
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public bool IsAvailable { get; set; }
        public string? Description { get; set; }
    }
}
