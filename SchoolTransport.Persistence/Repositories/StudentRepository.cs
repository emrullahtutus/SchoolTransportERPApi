using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.DTOs.Student;
using SchoolTransport.Application.DTOs.Vehicle;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Domain.Entities.Student;
using SchoolTransport.Domain.Entities.Vehicle;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class StudentRepository : RepositoryBase<Domain.Entities.Student.Student>, IStudentRepository
    {
        private readonly SchoolTransportDbContext _context;
        private readonly IVehicleRepository _vehicleRepository;

        public StudentRepository(SchoolTransportDbContext context, IVehicleRepository vehicleRepository) : base(context)
        {
            _context = context;
            _vehicleRepository = vehicleRepository;
        }

        public async Task SchoolToStudent(int schoolId, int studentId, string tenantId)
        {
            Student student = await _context.Students
                .SingleOrDefaultAsync(s => s.Id == studentId && s.TenantId == tenantId);

            if (student != null)
            {
                student.SchoolId = schoolId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Vehicle> GetStudentsAssignedToVehicle(int studentId, string tenantId)
        {
            var student = await _context.Students
                .Where(s => s.Id == studentId && s.TenantId == tenantId)
                .SingleOrDefaultAsync();

            if (student?.VehicleId == null) return null;

            return await _context.Vehicles
                .SingleOrDefaultAsync(v => v.Id == student.VehicleId && v.TenantId == tenantId);
        }

        public async Task<decimal?> GetMonthlyFeeByDistanceAsync(int schoolId, int distance, string tenantId)
        {
            var feeStructure = await _context.SchoolFeeStructures
                .Where(f => f.SchoolId == schoolId
                         && f.MinDistance <= distance
                         && f.MaxDistance >= distance
                         && f.IsActive
                         && f.TenantId == tenantId)
                .FirstOrDefaultAsync();

            return feeStructure?.MonthlyFee;
        }

        public async Task<List<Student>> GetStudentFee(int schoolId, int vehicleId, string tenantId)
        {
            var students = await _context.Students
                .Where(s => s.SchoolId == schoolId
                         && s.VehicleId == vehicleId
                         && s.TenantId == tenantId)
                .ToListAsync();

            return students;
        }

      
    }
}
