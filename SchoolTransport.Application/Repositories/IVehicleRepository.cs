using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        Task<List<School>> GetSchoolsByVehicleIdAsync(int vehicleId, string tenantId);
        Task AssignSchoolToThePlate(int vehicleId, int schoolId, string tenantId);
        Task<List<Vehicle>> GetVehicleBySchoolId(int schoolId, string tenantId);
    }
}