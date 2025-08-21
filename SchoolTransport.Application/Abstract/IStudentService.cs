// SchoolTransport.Application.Abstract/IStudentService.cs
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{
    public interface IStudentService
    {
        Task<List<StudentResponse>> GetAllAsync(string tenantId);
        Task<StudentResponse> GetByIdAsync(int id, string tenantId);
        Task<StudentResponse> CreateAsync(CreateStudentRequest request, string tenantId);
        Task<StudentResponse> UpdateAsync(int id, UpdateStudentRequest request, string tenantId);
        Task DeleteAsync(int id, string tenantId);
        Task AssignSchoolToStudentAsync(int studentId, int schoolId, string tenantId);
        Task AssignVehicleToStudentAsync(int studentId, int? vehicleId, string tenantId);
        Task<VehicleResponse> GetStudentsAssignedToVehicle(int studentId, string tenantId);
        Task<List<StudentFeeDto>> GetStudentFeeAsync(int schoolId, int vehicleId, string tenantId);
    }
}