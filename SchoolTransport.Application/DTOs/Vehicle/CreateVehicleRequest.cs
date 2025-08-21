using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Vehicle
{
    public class CreateVehicleRequest
    {
        public string PlateNumber { get; set; }
        public int? DriverId { get; set; }
        public List<int>? SchoolIds { get; set; }

    }
}
