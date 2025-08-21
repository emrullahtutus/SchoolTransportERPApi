using Microsoft.EntityFrameworkCore;
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
    public class VehicleRepository : RepositoryBase<Domain.Entities.Vehicle.Vehicle>, IVehicleRepository
    {
        private readonly SchoolTransportDbContext _context;

        public VehicleRepository(SchoolTransportDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<School>> GetSchoolsByVehicleIdAsync(int vehicleId, string tenantId)
        {
            var schools = await _context.Vehicles
                .Where(v => v.Id == vehicleId && v.TenantId == tenantId)
                .SelectMany(v => v.Schools)
                .Where(s => s.TenantId == tenantId)
                .ToListAsync();

            foreach (var item in schools)
            {
                await Console.Out.WriteLineAsync(item.Name);
            }
            return schools;
        }

        public async Task<List<Student>> GetStudentsBySchoolIdAsync(int schoolId, string tenantId)
        {
            var students = await _context.Students
                .Where(s => s.SchoolId == schoolId && s.TenantId == tenantId)
                .ToListAsync();
            return students;
        }

        public async Task AssignSchoolToThePlate(int vehicleId, int schoolId, string tenantId)
        {
            // 1. Araç var mı kontrol et
            var vehicle = await _context.Vehicles
                .Include(v => v.Schools)
                .FirstOrDefaultAsync(v => v.Id == vehicleId && v.TenantId == tenantId);
            var school = await _context.Schools.FirstOrDefaultAsync(s => s.Id == schoolId && s.TenantId == tenantId);
         
            // 3. İlişki zaten var mı DB üzerinden kontrol et
            bool alreadyAssigned = await _context.Vehicles
                .Where(v => v.Id == vehicleId && v.TenantId == tenantId)
                .SelectMany(v => v.Schools)
                .AnyAsync(s => s.Id == schoolId && s.TenantId == tenantId);

            if (alreadyAssigned)
                return; // Zaten kayıtlı, tekrar ekleme
            vehicle.Schools.Add(school);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vehicle>> GetVehicleBySchoolId(int schoolId, string tenantId)
        {
            var vehicles = await _context.Vehicles
                .Include(v => v.Schools)
                .Where(v => v.Schools.Any(s => s.Id == schoolId && s.TenantId == tenantId) && v.TenantId == tenantId)
                .ToListAsync();
            return vehicles;
        }
    }

}
