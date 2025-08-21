using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.School
{
    public class School : BaseEntity
    {
        public string Name { get; set; } // Okul adı - Required

        // Student ile N-1 ilişki
        public ICollection<Student.Student>? Students { get; set; }

        // Vehicle ile N-N ilişki (Bir okul birden fazla araç kullanabilir, bir araç birden fazla okula hizmet verebilir)
        public ICollection<Vehicle.Vehicle>? Vehicles { get; set; }

        public int AcademicYear { get; set; } // 2024 (2024-2025 için)
        public DateTime AcademicYearStartDate { get; set; } // Eylül başı
        public DateTime AcademicYearEndDate { get; set; } // Mayıs sonu
        public int InstallmentCount { get; set; } = 9; // 9 ay

        public ICollection<SchoolFeeStructure> FeeStructures { get; set; } = new List<SchoolFeeStructure>();
    }
}
