namespace SchoolTransport.Application.DTOs.School
{
    public class SchoolFeeStructureResponse
    {
        public decimal MinDistance { get; set; }
        public decimal MaxDistance { get; set; }
        public decimal MonthlyFee { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
