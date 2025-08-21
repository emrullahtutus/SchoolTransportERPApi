using SchoolTransport.Application.DTOs.Activity;
using SchoolTransport.Application.DTOs.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IActivityService
    {
        Task<List<ActivityResponseDto>> GetAllActivityAsync(string tenantId);
       Task<ActivityResponseDto> GetByIdActivityAsync(int id,string tenantId);  
        Task<ActivityResponseDto> CreateActivityAsync(CreateActivityRequest request, string tenantId);
        Task UpdateActivityAsync(UpdateActivityRequest request, string tenantId);
        Task DeleteActivityAsync(int id, string tenantId);
        Task<List<ActivityResponseDto>> VehicleByActivity(int vehicleId, string tenantId);
    }
}
