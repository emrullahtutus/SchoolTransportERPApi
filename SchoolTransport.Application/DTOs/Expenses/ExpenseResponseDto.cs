using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Expenses
{
    public class ExpenseResponseDto
    {
        public int Id { get; set; }
        public decimal? Fuel { get; set; }
        public decimal? Penalty { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Industry { get; set; }
        public decimal? Salary { get; set; }
        public DateTime DateTime { get; set; }
        public string Notes { get; set; }
    }
}
