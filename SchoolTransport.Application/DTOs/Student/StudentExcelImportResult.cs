using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Student
{
    public class StudentExcelImportResult
    {
        public bool IsSuccess { get; set; }
        public int TotalProcessed { get; set; }
        public int SuccessfullyAdded { get; set; }
        public List<string> Errors { get; set; } = new();
        public List<string> Warnings { get; set; } = new();
    }
}
