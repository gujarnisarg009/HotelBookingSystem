using HotelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Application.DTOs.Rooms
{
    public class CreateRoomDto
    {
        //Admin Use For...
        public string RoomNumber { get; set; } = string.Empty;
        public RoomType Type { get; set; }
        public decimal PricePerNight { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
    }
}
