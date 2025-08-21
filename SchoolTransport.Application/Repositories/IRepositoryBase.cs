using SchoolTransport.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<T> GetByIdAsync(int id, string tenantId, bool tracking = false);
        Task<List<T>> GetAllAsync(string tenantId, bool tracking = false);
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, string tenantId, bool tracking = false);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteByIdAsync(int id, string tenantId);
        Task<int> SaveChangesAsync();
    }
}