using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBooking.Domain.Enums
{
    public enum BookingStatus
    {
        Pending = 1,
        Confirmed = 2,
        Cancelled = 3,
        CheckedIn = 4,
        CheckedOut = 5
    }
}
