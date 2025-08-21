using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.School;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly SchoolTransportDbContext _context;

        public ReportRepository(SchoolTransportDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetAllSchoolCountAsync(string tenantId)
        {
            return await _context.Schools.CountAsync(s => s.TenantId == tenantId);
        }

        public async Task<int> GetAllStudentCountAsync(string tenantId)
        {
            return await _context.Students.CountAsync(s => s.TenantId == tenantId);
        }

        public async Task<int> GetAllVehicleCountAsync(string tenantId)
        {
            return await _context.Vehicles.CountAsync(v => v.TenantId == tenantId);
        }

        public async Task<int> GetVehicleAllStudentCountAsync(int vehicleId, string tenantId)
        {
            return await _context.Students
                .Where(v => v.VehicleId == vehicleId && v.TenantId == tenantId)
                .CountAsync();
        }

        public async Task<int> GetSchoolCountAsync(int vehicleId, string tenantId)
        {
            return await _context.Vehicles
                .Where(v => v.Id == vehicleId && v.TenantId == tenantId)
                .Select(v => v.Schools.Count)
                .FirstOrDefaultAsync();
        }

        public async Task<List<School>> GetVehivleSchoolList(int vehicleId, string tenantId)
        {
            return await _context.Vehicles
                .Where(v => v.Id == vehicleId && v.TenantId == tenantId)
                .Include(v => v.Schools)
                .SelectMany(v => v.Schools)
                .Where(s => s.TenantId == tenantId)
                .Distinct()
                .ToListAsync();
        }

        public async Task<int> GetVehicleSchoolStudentCountAsync(int vehicleId, int schoolId, string tenantId)
        {
            return await _context.Students
                .Include(s => s.School)
                .Include(s => s.Vehicle)
                .Where(s => s.Vehicle.Id == vehicleId && s.SchoolId == schoolId && s.TenantId == tenantId)
                .CountAsync();
        }

        public async Task<int> GetSchoolAllStudentsCountAsync(int schoolId, string tenantId)
        {
            return await _context.Students
                .Where(s => s.SchoolId == schoolId && s.TenantId == tenantId)
                .CountAsync();
        }
    }
    }
