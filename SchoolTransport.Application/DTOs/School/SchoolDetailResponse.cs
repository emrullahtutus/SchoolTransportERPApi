using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.School
{
    public class SchoolDetailResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<StudentResponse>? Students { get; set; }
        public List<VehicleResponse>? Vehicles { get; set; }
    }
}
