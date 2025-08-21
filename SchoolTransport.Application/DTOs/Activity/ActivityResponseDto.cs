using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.DTOs.Activity
{
    public class ActivityResponseDto
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string? Description { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
