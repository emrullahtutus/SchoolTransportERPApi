using SchoolTransport.Application.DTOs.Driver;
using SchoolTransport.Application.DTOs.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Vehicle
{
    public class VehicleDetailResponse
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; }

        public DriverResponse? Driver { get; set; }
        public List<SchoolResponse>? Schools { get; set; }
    }
}
