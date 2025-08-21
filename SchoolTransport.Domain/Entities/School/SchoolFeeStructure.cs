using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.School
{
    public class SchoolFeeStructure : BaseEntity
    {
        public int SchoolId { get; set; }
        public School School { get; set; }

        // Mesafe aralığı
        public decimal MinDistance { get; set; } // Minimum km (dahil)
        public decimal MaxDistance { get; set; } // Maksimum km (dahil değil)

        // Ücret
        public decimal MonthlyFee { get; set; } // Bu aralık için aylık ücret

        // Ek bilgiler
        public string? Description { get; set; } // "0-3 km arası"
        public bool IsActive { get; set; } = true;
    }
}
