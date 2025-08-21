using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface ISchoolRepository : IRepositoryBase<Domain.Entities.School.School>
    {
        Task<List<Student>> GetStudentsBySchoolIdAsync(int schoolId, string tenantId);
        Task<School> GetSchoolWithFeeStructuresAsync(int id, string tenantId);
        Task UpdateSchoolWithFeeStructuresAsync(int id, string tenantId, UpdateSchoolRequest request);
        Task<List<School>> GetSchoolsByVehicleAsync(int vehicleId, string tenantId);
        Task<School> GetSchoolWithPeriodsAsync(int schoolId, string tenantId);
        Task<List<Student>> GetStudentsVehicleAndSchool(int vehicleId, int schoolId, string tenantId);
        Task<List<SchoolFeeStructure>> GetSchoolFeeStructuresAsync(int schoolId, string tenantId);
        Task<School> GetSchoolStudentById(int studentId, string tenantId);
        Task<School> CreateSchoolAsync(CreateSchoolRequest request, string tenantId);
        Task<IEnumerable<School>> GetSchoolsByNamesAsync(IEnumerable<string> names, string tenantId);

    }
}