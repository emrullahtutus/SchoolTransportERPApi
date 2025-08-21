using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.School
{
    public class UpdateSchoolRequest
    {
        public string Name { get; set; }
        public int AcademicYear { get; set; }
        public DateTime AcademicYearStartDate { get; set; }
        public DateTime AcademicYearEndDate { get; set; }
        public List<SchoolFeeStructureDto> FeeStructures { get; set; }
    
    }
}
