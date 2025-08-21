using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.DTOs.Activity;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Activity;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository
    {

        public ActivityRepository(SchoolTransportDbContext context) : base(context)
        {
        }

        public async Task<List<Activity>> VehicleByActivity(int vehicleId, string tenantId)
        {
            return await _context.Activities.Where(a => a.TenantId == tenantId && a.VehicleId == vehicleId)
                       .ToListAsync();
        }

        public async Task<List<Activity>> GetActivitiesByDaysAheadAsync(string tenantId, int daysAhead)
        {
            var now = DateTime.Now;
            var futureDate = now.AddDays(daysAhead);

            return await _context.Activities
                .Where(a => a.TenantId == tenantId &&
                           a.IsActive == true &&
                           a.DateTime >= now &&
                           a.DateTime <= futureDate)
                .OrderBy(a => a.DateTime)
                .ToListAsync();
        }

    
    }
}
