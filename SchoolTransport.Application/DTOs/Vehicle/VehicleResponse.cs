using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Vehicle
{
    public class VehicleResponse
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }

        public int? DriverId { get; set; }
        public string? DriverName { get; set; }
        public bool HasDriver => DriverId.HasValue;
        public int SchoolCount { get; set; }
    }
}
