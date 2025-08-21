using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IVehicleService
    {
        Task<List<VehicleResponse>> GetAllVehiclesAsync(string tenantId);
        Task<VehicleDetailResponse> GetVehicleDetailByIdAsync(int id, string tenantId);
        Task<VehicleResponse> CreateVehicleAsync(CreateVehicleRequest request, string tenantId);
        Task UpdateVehicleAsync(int id, UpdateVehicleRequest request, string tenantId);
        Task DeleteVehicleAsync(int id, string tenantId);
        Task<List<SchoolResponse>> GetSchoolsByVehicleIdAsync(int vehicleId, string tenantId);
        Task AssignSchoolToVehicleAsync(int vehicleId, int schoolId, string tenantId);
        Task AssignDriverToVehicleAsync(int vehicleId, int driverId, string tenantId);
        Task RemoveDriverFromVehicleAsync(int vehicleId, string tenantId);
        Task<List<VehicleResponse>> GetVehicleBySchoolId(int schoolId, string tenantId);
    }
}
