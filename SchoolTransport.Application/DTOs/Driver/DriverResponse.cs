using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Driver
{
    public class DriverResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? VehiclePlateNumber { get; set; }
        public int? VehicleId { get; set; }
        public bool HasVehicle => VehicleId.HasValue;
    }
}
