using SchoolTransport.Domain.Entities.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {
      Task<List<Activity>> VehicleByActivity(int vehicleId, string tenantId);  

    }
}
