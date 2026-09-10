using HotelBooking.Domain.Common;
using HotelBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Customer;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
