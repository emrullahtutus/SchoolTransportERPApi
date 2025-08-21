using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.School
{
    public class PeriodInfo
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public string MonthName { get; set; }
        public int PeriodNumber { get; set; } // Kaçıncı ay (1, 2, 3...)
        public string PeriodDate { get; set; }
    }
}
