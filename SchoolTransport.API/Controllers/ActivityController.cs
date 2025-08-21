using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Activity;

namespace SchoolTransport.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]    

    public class ActivityController : BaseController
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllActivities()
        {
            try
            {
                var tenantId = GetTenantId();
                var activities = await _activityService.GetAllActivityAsync(tenantId);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromBody] CreateActivityRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tenantId = GetTenantId();
                var result = await _activityService.CreateActivityAsync(request, tenantId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
            [HttpGet("{id:int}")]
            public async Task<IActionResult> GetByIdActivitiy(int id)
            {
                var tenantId = GetTenantId();
                var result=await _activityService.GetByIdActivityAsync(id, tenantId);
                return Ok(result);
            }

        [HttpPut]
        public async Task<IActionResult> UpdateActivity([FromBody] UpdateActivityRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var tenantId = GetTenantId();
                await _activityService.UpdateActivityAsync(request, tenantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var tenantId = GetTenantId();
                await _activityService.DeleteActivityAsync(id, tenantId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetActivitiesByVehicle(int vehicleId)
        {
            try
            {
                var tenantId = GetTenantId();
                var activities = await _activityService.VehicleByActivity(vehicleId, tenantId);
                return Ok(activities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

