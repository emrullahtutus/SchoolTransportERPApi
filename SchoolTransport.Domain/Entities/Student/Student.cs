using SchoolTransport.Domain.Common;
using SchoolTransport.Domain.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Domain.Entities.Student
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; } // Öğrencinin adı soyadı - Required
        public string PhoneNumber { get; set; } // Öğrenci telefon numarası - Required
        public string Address { get; set; }

        // School ile 1-N ilişki (Bir okulun birden fazla öğrencisi, her öğrencinin bir okulu)
        public int SchoolId { get; set; }
        public School.School? School { get; set; }
        public int Distance { get; set; }
        public decimal? MonthlyFee { get; set; } 
        public Payment.Payment? Payment { get; set; }

        public int? VehicleId { get; set; } 
        public Vehicle.Vehicle? Vehicle { get; set; }

        public ICollection<PaymentTransaction> PaymentTransaction { get; set; }

    }
}
