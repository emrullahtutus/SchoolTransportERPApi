using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IStudentRepository : IRepositoryBase<Domain.Entities.Student.Student>
    {
        Task SchoolToStudent(int schoolId, int studentId, string tenantId);
        Task<decimal?> GetMonthlyFeeByDistanceAsync(int schoolId, int distance, string tenantId);
        Task<Vehicle> GetStudentsAssignedToVehicle(int studentId, string tenantId);
        Task<List<Student>> GetStudentFee(int schoolId, int vehicleId, string tenantId);
      
    }
}