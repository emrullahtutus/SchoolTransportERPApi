using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Vehicle
{
    public class Vehicle : BaseEntity
    {
        public string PlateNumber { get; set; } // Araç plakası - Required

        // Driver ile 1-1 ilişki (Her aracı sadece bir şoför kullanabilir, nullable çünkü araç boşta olabilir)
        public int? DriverId { get; set; }
        public Driver.Driver? Driver { get; set; }

        // School ile N-N ilişki
        public ICollection<School.School>? Schools { get; set; }
        public ICollection<Student.Student>? Students { get; set; }

        public ICollection<Expenses.Expense> Expenses { get; set; }


        public ICollection<Activity.Activity> Activities { get; set; }
    }
}
