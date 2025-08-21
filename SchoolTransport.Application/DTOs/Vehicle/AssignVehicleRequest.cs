using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Vehicle
{
    public class AssignVehicleRequest
    {
        public int StudentId { get; set; }
        public int? VehicleId { get; set; } // null = araç atamasını kaldır

    }
}
