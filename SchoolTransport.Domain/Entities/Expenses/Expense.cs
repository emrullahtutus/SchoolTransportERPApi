using SchoolTransport.Domain.Common;
using SchoolTransport.Domain.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Expenses
{
    public class Expense:BaseEntity
    {
        public decimal? Fuel { get; set; }
        public decimal? Penalty { get; set; }
        public decimal? Insurance { get; set; }
        public decimal? Industry { get; set; }
        public decimal? Salary { get; set; }
        public DateTime DateTime { get; set; }
        public string Notes { get; set; }
        public int VehicleId { get; set; }
        public Vehicle.Vehicle Vehicle { get; set; }

    }
}
