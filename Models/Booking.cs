using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTogether.Models
{
    internal class Booking
    {
        public int BookingID { get; set; }
        public int RideID { get; set; }
        public int PassengerID { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
