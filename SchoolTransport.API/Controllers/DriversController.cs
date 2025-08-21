// BaseController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Driver;
using System.Security.Claims;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]

    public class DriversController : BaseController
    {
        private readonly IDriverService _driverService;

        public DriversController(IDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DriverResponse>>> GetAll()
        {
            var tenantId = GetTenantId();
            var drivers = await _driverService.GetAllDriversAsync(tenantId);
            return Ok(drivers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DriverResponse>> GetById(int id)
        {
            var tenantId = GetTenantId();
            var driver = await _driverService.GetDriverByIdAsync(id, tenantId);
            if (driver == null)
                return NotFound();
            return Ok(driver);
        }

        [HttpPost]
        public async Task<ActionResult<DriverResponse>> Create([FromBody] CreateDriverRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            var createdDriver = await _driverService.CreateDriverAsync(request, tenantId);
            return CreatedAtAction(nameof(GetById), new { id = createdDriver.Id }, createdDriver);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateDriverRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            await _driverService.UpdateDriverAsync(id, request, tenantId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tenantId = GetTenantId();
            await _driverService.DeleteDriverAsync(id, tenantId);
            return NoContent();
     
       }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }
    }
}
