using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.School
{
    public class MonthlyPeriodsResponse
    {
        public string SchoolName { get; set; }
     
        public List<PeriodInfo> Periods { get; set; }
    }
}
