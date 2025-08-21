﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Student
{
    public class StudentExcelDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string SchoolName { get; set; } // School Name ile işlem yapacağız
        public int Distance { get; set; }
        
    }
}
