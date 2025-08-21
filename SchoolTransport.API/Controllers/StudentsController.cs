using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
   
    public class StudentsController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentResponse>>> GetAll()
        {
            var tenantId = GetTenantId();
            var students = await _studentService.GetAllAsync(tenantId);
            return Ok(students);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudentResponse>> GetById(int id)
        {
            var tenantId = GetTenantId();
            var student = await _studentService.GetByIdAsync(id, tenantId);
            if (student == null)
                return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentResponse>> Create([FromBody] CreateStudentRequest createStudentRequest)
        {
            var tenantId = GetTenantId();
            var result = await _studentService.CreateAsync(createStudentRequest, tenantId);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<StudentResponse>> Update(int id, [FromBody] UpdateStudentRequest request)
        {
            var tenantId = GetTenantId();
            var result = await _studentService.UpdateAsync(id, request, tenantId);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tenantId = GetTenantId();
            await _studentService.DeleteAsync(id, tenantId);
            return NoContent();
        }

        [HttpPost("{studentId:int}/assign-school/{schoolId:int}")]
        public async Task<IActionResult> AssignSchoolToStudent(int studentId, int schoolId)
        {
            var tenantId = GetTenantId();
            await _studentService.AssignSchoolToStudentAsync(studentId, schoolId, tenantId);
            return NoContent();
        }

        [HttpPut("assign-vehicle/{studentId}/{vehicleId}")]
        public async Task<IActionResult> AssignVehicleToStudent(int studentId, int vehicleId)
        {
            var tenantId = GetTenantId();
            await _studentService.AssignVehicleToStudentAsync(studentId, vehicleId, tenantId);
            return Ok();
        }

        [HttpGet("{studentId:int}/vehicle")]
        public async Task<IActionResult> GetStudentAssignedVehicle(int studentId)
        {
            var tenantId = GetTenantId();
            VehicleResponse vehicle = await _studentService.GetStudentsAssignedToVehicle(studentId, tenantId);
            return Ok(vehicle?.PlateNumber ?? "Atanmamış");
        }

        [HttpGet("fees/school/{schoolId:int}/vehicle/{vehicleId:int}")]
        public async Task<IActionResult> GetStudentFee(int schoolId, int vehicleId)
        {
            var tenantId = GetTenantId();
            var result = await _studentService.GetStudentFeeAsync(schoolId, vehicleId, tenantId);
            return Ok(result);
        }
    }
}