using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Vehicle;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class VehiclesController : BaseController
    {
        private readonly IVehicleService _vehicleService;
        private readonly ISchoolService _schoolService;

        public VehiclesController(IVehicleService vehicleService, ISchoolService schoolService)
        {
            _vehicleService = vehicleService;
            _schoolService = schoolService;
        }

        [HttpGet]
        public async Task<ActionResult<List<VehicleResponse>>> GetAll()
        {
            var tenantId = GetTenantId();
            var vehicles = await _vehicleService.GetAllVehiclesAsync(tenantId);
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleDetailResponse>> GetById(int id)
        {
            var tenantId = GetTenantId();
            var vehicle = await _vehicleService.GetVehicleDetailByIdAsync(id, tenantId);
            if (vehicle == null)
                return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<ActionResult<VehicleResponse>> Create([FromBody] CreateVehicleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            var createdVehicle = await _vehicleService.CreateVehicleAsync(request, tenantId);
            return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateVehicleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            await _vehicleService.UpdateVehicleAsync(id, request, tenantId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tenantId = GetTenantId();
            await _vehicleService.DeleteVehicleAsync(id, tenantId);
            return NoContent();
        }

        [HttpGet("{vehicleId}/schools")]
        public async Task<ActionResult<List<SchoolResponse>>> GetSchoolsByVehicle(int vehicleId)
        {
            var tenantId = GetTenantId();
            var schools = await _vehicleService.GetSchoolsByVehicleIdAsync(vehicleId, tenantId);
            return Ok(schools);
        }

        [HttpGet("{schoolId}/students")]
        public async Task<IActionResult> GetStudentsBySchool(int schoolId)
        {
            var tenantId = GetTenantId();
            var students = await _schoolService.GetStudentsBySchoolIdAsync(schoolId, tenantId);
            return Ok(students);
        }

        [HttpPost("{vehicleId}/assign-school/{schoolId}")]
        public async Task<IActionResult> AssignSchoolToVehicle(int vehicleId, int schoolId)
        {
            try
            {
                var tenantId = GetTenantId();
                await _vehicleService.AssignSchoolToVehicleAsync(vehicleId, schoolId, tenantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{vehicleId}/assign-driver/{driverId}")]
        public async Task<ActionResult> AssignDriver(int vehicleId, int driverId)
        {
            var tenantId = GetTenantId();
            await _vehicleService.AssignDriverToVehicleAsync(vehicleId, driverId, tenantId);
            return NoContent();
        }

        [HttpPost("{vehicleId}/remove-driver")]
        public async Task<ActionResult> RemoveDriver(int vehicleId)
        {
            var tenantId = GetTenantId();
            await _vehicleService.RemoveDriverFromVehicleAsync(vehicleId, tenantId);
            return NoContent();
        }

        [HttpGet("schools/{schoolId}/vehicles")]
        public async Task<IActionResult> GetSchoolVehicle(int schoolId)
        {
            var tenantId = GetTenantId();
            var vehicle = await _vehicleService.GetVehicleBySchoolId(schoolId, tenantId);
            return Ok(vehicle);
        }
    }
}