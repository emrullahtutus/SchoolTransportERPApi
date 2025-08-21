using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.School
{
    public class SchoolFeeStructureDto
    {
        public decimal MinDistance { get; set; }
        public decimal MaxDistance { get; set; }
        public decimal MonthlyFee { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
