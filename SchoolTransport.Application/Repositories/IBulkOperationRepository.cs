using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IBulkOperationRepository
    {
        Task AddRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class;
        Task UpdateRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class;
        Task DeleteRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class;
    }
}
