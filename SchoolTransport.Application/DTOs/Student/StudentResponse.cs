using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Student
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public int SchoolId { get; set; }
        public int Distance { get; set; }
        public decimal? MonthlyFee { get; set; } // hesaplanan ücret
        public int VehicleId { get; set; }
    }
}

