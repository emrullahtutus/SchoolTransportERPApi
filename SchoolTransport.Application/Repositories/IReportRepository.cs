using SchoolTransport.Domain.Entities.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IReportRepository
    {
        Task<int> GetAllStudentCountAsync(string tenantId);
        Task<int> GetAllVehicleCountAsync(string tenantId);
        Task<int> GetAllSchoolCountAsync(string tenantId);
        Task<int> GetSchoolCountAsync(int vehicleId, string tenantId);
        Task<List<School>> GetVehivleSchoolList(int vehicleId, string tenantId);
        Task<int> GetVehicleAllStudentCountAsync(int vehicleId, string tenantId);
        Task<int> GetVehicleSchoolStudentCountAsync(int vehicleId, int schoolId, string tenantId);
        Task<int> GetSchoolAllStudentsCountAsync(int schoolId, string tenantId);
    }
}