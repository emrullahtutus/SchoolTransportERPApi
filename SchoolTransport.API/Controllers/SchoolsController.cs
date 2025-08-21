using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class SchoolsController : BaseController
    {
        private readonly ISchoolService _schoolService;

        public SchoolsController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SchoolResponse>>> GetAll()
        {
            var tenantId = GetTenantId();
            var schools = await _schoolService.GetAllSchoolsAsync(tenantId);
            return Ok(schools);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SchoolDetailResponse>> GetById(int id)
        {
            var tenantId = GetTenantId();
            var school = await _schoolService.GetSchoolDetailByIdAsync(id, tenantId);
            if (school == null)
                return NotFound();
            return Ok(school);
        }

        [HttpPost]
        public async Task<ActionResult<SchoolResponse>> Create([FromBody] CreateSchoolRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            var createdSchool = await _schoolService.CreateSchoolAsync(request, tenantId);
            return CreatedAtAction(nameof(GetById), new { id = createdSchool.Id }, createdSchool);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSchoolRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            await _schoolService.UpdateSchoolAsync(id, request, tenantId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tenantId = GetTenantId();
            await _schoolService.DeleteSchoolAsync(id, tenantId);
            return NoContent();
        }

        [HttpGet("{schoolId}/students")]
        public async Task<ActionResult<List<StudentResponse>>> GetStudentsBySchoolId(int schoolId)
        {
            var tenantId = GetTenantId();
            var students = await _schoolService.GetStudentsBySchoolIdAsync(schoolId, tenantId);
            return Ok(students);
        }

        [HttpGet("vehicles/{vehicleId}/schools")]
        public async Task<IActionResult> GetSchoolsByVehicle(int vehicleId)
        {
            var tenantId = GetTenantId();
            var schools = await _schoolService.GetSchoolsByVehicleAsync(vehicleId, tenantId);
            return Ok(schools);
        }

        [HttpGet("schools/{schoolId}/periods")]
        public async Task<IActionResult> GetSchoolWithPeriods(int schoolId)
        {
            var tenantId = GetTenantId();
            var result = await _schoolService.GetSchoolWithPeriodsAsync(schoolId, tenantId);
            return Ok(result);
        }

        [HttpGet("vehicles/{vehicleId}/schools/{schoolId}/students")]
        public async Task<IActionResult> GetStudentsByVehicleAndSchool(int vehicleId, int schoolId)
        {
            var tenantId = GetTenantId();
            var students = await _schoolService.GetStudentsVehicleAndSchool(vehicleId, schoolId, tenantId);
            if (students == null || !students.Any())
                return NotFound($"Araç ID: {vehicleId} ve Okul ID: {schoolId} için öğrenci bulunamadı");
            return Ok(students);
        }

        [HttpGet("{schoolId}/Price")]
        public async Task<IActionResult> GetSchoolFeeStructuresAsync(int schoolId)
        {
            var tenantId = GetTenantId();
            var schoolFeeStructures = await _schoolService.GetSchoolFeeStructuresAsync(schoolId, tenantId);
            return Ok(schoolFeeStructures);
        }
    }
}