using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
   
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("studentCount")]
        public async Task<IActionResult> GetAllStudentCountAsync()
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetAllStudentCountAsync(tenantId);
            return Ok(count);
        }

        [HttpGet("vehiclesCount")]
        public async Task<IActionResult> GetAllVehicleCountAsync()
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetAllVehicleCountAsync(tenantId);
            return Ok(count);
        }

        [HttpGet("schoolCount")]
        public async Task<IActionResult> GetAllSchoolCountAsync()
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetAllSchoolCountAsync(tenantId);
            return Ok(count);
        }

        [HttpGet("{vehicleId:int}/vehicleCountSchools")]
        public async Task<IActionResult> GetSchoolCountByVehicle(int vehicleId)
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetSchoolCountAsync(vehicleId, tenantId);
            return Ok(count);
        }

        [HttpGet("{vehicleId:int}/vehicleSchoolList")]
        public async Task<IActionResult> GetVehivleSchoolList(int vehicleId)
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetVehivleSchoolList(vehicleId, tenantId);
            return Ok(count);
        }

        [HttpGet("{vehicleId:int}/vehicleAllStudentCount")]
        public async Task<IActionResult> GetVehicleAllStudentCountAsync(int vehicleId)
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetVehicleAllStudentCountAsync(vehicleId, tenantId);
            return Ok(count);
        }

        [HttpGet("{vehicleId:int}/{schoolId:int}/vehicleSchoolStudentCount")]
        public async Task<IActionResult> GetVehicleSchoolStudentCount(int vehicleId, int schoolId)
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetVehicleSchoolStudentCountAsync(vehicleId, schoolId, tenantId);
            return Ok(count);
        }

        [HttpGet("{schoolId:int}/schoolStudentsCount")]
        public async Task<IActionResult> GetSchoolAllStudentsCountAsync(int schoolId)
        {
            var tenantId = GetTenantId();
            var count = await _reportService.GetSchoolAllStudentsCountAsync(schoolId, tenantId);
            return Ok(count);
        }
    }
}
