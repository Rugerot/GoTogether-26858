using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoTogether.Models
{
    internal class Ride
    {
        public int RideID { get; set; }
        public int DriverID { get; set; }
        public string DriverName { get; set; } 
        public string Route { get; set; }
        public string DepartureTime { get; set; }
        public string VehicleInfo { get; set; }
        public int AvailableSeats { get; set; }
        public string Status { get; set; }
    }
}
