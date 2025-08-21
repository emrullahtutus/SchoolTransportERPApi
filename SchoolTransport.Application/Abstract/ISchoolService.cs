// SchoolTransport.Application.Abstract/ISchoolService.cs
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Domain.Entities.School;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface ISchoolService
    {
        Task<List<SchoolResponse>> GetAllSchoolsAsync(string tenantId);
        Task<SchoolDetailResponse> GetSchoolDetailByIdAsync(int id, string tenantId);
        Task<SchoolResponse> CreateSchoolAsync(CreateSchoolRequest request, string tenantId);
        Task UpdateSchoolAsync(int id, UpdateSchoolRequest request, string tenantId);
        Task DeleteSchoolAsync(int id, string tenantId);
        Task<List<StudentResponse>> GetStudentsBySchoolIdAsync(int schoolId, string tenantId);
        Task<List<SchoolResponse>> GetSchoolsByVehicleAsync(int vehicleId, string tenantId);
        Task<MonthlyPeriodsResponse> GetSchoolWithPeriodsAsync(int schoolId, string tenantId);
        Task<List<StudentResponse>> GetStudentsVehicleAndSchool(int vehicleId, int schoolId, string tenantId);
        Task<List<SchoolFeeStructureResponse>> GetSchoolFeeStructuresAsync(int schoolId, string tenantId);
      
    }
}