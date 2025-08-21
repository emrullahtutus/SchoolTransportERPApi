using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Persistence.Context;
using Microsoft.EntityFrameworkCore;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolTransport.Application.DTOs.School;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Application.Exceptions.NotFound;

namespace SchoolTransport.Persistence.Repositories
{
    public class SchoolRepository : RepositoryBase<Domain.Entities.School.School>, ISchoolRepository
    {
        private readonly SchoolTransportDbContext _context;

        public SchoolRepository(SchoolTransportDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsBySchoolIdAsync(int schoolId, string tenantId)
        {
            return await _context.Students
                .Where(s => s.SchoolId == schoolId && s.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<School> GetSchoolWithFeeStructuresAsync(int id, string tenantId)
        {
            return await _context.Schools
                .Include(s => s.FeeStructures)
                .FirstOrDefaultAsync(s => s.Id == id && s.TenantId == tenantId);
        }
      
        public async Task UpdateSchoolWithFeeStructuresAsync(int id, string tenantId, UpdateSchoolRequest request)
        {
            var school = await GetSchoolWithFeeStructuresAsync(id, tenantId);
            if (school != null)
            {
                school.Name = request.Name;
                school.AcademicYear = request.AcademicYear;
                school.AcademicYearStartDate = request.AcademicYearStartDate;
                school.AcademicYearEndDate = request.AcademicYearEndDate;

                _context.SchoolFeeStructures.RemoveRange(school.FeeStructures);

                foreach (var feeReq in request.FeeStructures)
                {
                    school.FeeStructures.Add(new SchoolFeeStructure
                    {
                        MinDistance = feeReq.MinDistance,
                        MaxDistance = feeReq.MaxDistance,
                        MonthlyFee = feeReq.MonthlyFee,
                        Description = feeReq.Description,
                        IsActive = feeReq.IsActive,
                        SchoolId = id,
                        TenantId = tenantId
                    });
                }
         
                await SaveChangesAsync();
            }
        }

        public async Task<List<School>> GetSchoolsByVehicleAsync(int vehicleId, string tenantId)
        {
            return await _context.Students
                .Where(s => s.VehicleId == vehicleId && s.TenantId == tenantId)
                .Select(s => s.School)
                .Where(s => s.TenantId == tenantId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<School> GetSchoolWithPeriodsAsync(int schoolId, string tenantId)
        {
            return await _context.Schools
                .FirstOrDefaultAsync(s => s.Id == schoolId && s.TenantId == tenantId);
        }

        public async Task<List<Student>> GetStudentsVehicleAndSchool(int vehicleId, int schoolId, string tenantId)
        {
            return await _context.Students
                .Include(s => s.School)
                .Include(s => s.Vehicle)
                .Where(s => s.Vehicle.Id == vehicleId && s.SchoolId == schoolId && s.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<List<SchoolFeeStructure>> GetSchoolFeeStructuresAsync(int schoolId, string tenantId)
        {
            var schoolFeeStructure = await _context.SchoolFeeStructures
                .Where(s => s.SchoolId == schoolId && s.TenantId == tenantId)
                .ToListAsync();
            return schoolFeeStructure;
        }

        public async Task<School> GetSchoolStudentById(int studentId, string tenantId)
        {
            var student = await _context.Students
                .Include(s => s.School)
                .SingleOrDefaultAsync(s => s.Id == studentId && s.TenantId == tenantId);
            return student?.School;
        }

        public async Task<School> CreateSchoolAsync(CreateSchoolRequest request, string tenantId)
        {
            var school = new School
            {
                Name = request.Name,
                AcademicYear = request.AcademicYear,
                AcademicYearStartDate = request.AcademicYearStartDate,
                AcademicYearEndDate = request.AcademicYearEndDate,
                TenantId = tenantId
            };

            // FeeStructures ekle
            foreach (var feeReq in request.FeeStructures)
            {
                school.FeeStructures.Add(new SchoolFeeStructure
                {
                    MinDistance = feeReq.MinDistance,
                    MaxDistance = feeReq.MaxDistance,
                    MonthlyFee = feeReq.MonthlyFee,
                    Description = feeReq.Description,
                    IsActive = feeReq.IsActive,
                    TenantId = tenantId // Bunu da unutma!
                });
            }

            _context.Schools.Add(school);
            await SaveChangesAsync();
            return school;
        }

        
    public async Task<IEnumerable<School>> GetSchoolsByNamesAsync(IEnumerable<string> names, string tenantId)
    {
        // MANUEL FİLTRELEME: tenantId'ye göre filtreleme yapıyoruz.
        return await _context.Schools
            .Where(s => names.Contains(s.Name) && s.TenantId == tenantId)
            .ToListAsync();
    }
    }
}
