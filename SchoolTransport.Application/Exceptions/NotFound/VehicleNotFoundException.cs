using SchoolTransport.Application.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Exceptions.NotFound
{
    public class VehicleNotFoundException : BaseNotFoundException
    {
        public VehicleNotFoundException(Guid vehicleId) : base($"Id: {vehicleId} olan araç bulunamadı.") { }
        public VehicleNotFoundException(string message) : base(message) { }
    }
}
