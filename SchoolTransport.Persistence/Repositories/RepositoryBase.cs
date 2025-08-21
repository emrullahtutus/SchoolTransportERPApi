using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Common;
using SchoolTransport.Persistence.Context;
using System.Linq.Expressions;

public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
{
    protected readonly SchoolTransportDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(SchoolTransportDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id, string tenantId, bool tracking = false)
    {
        if (tracking)
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
    }

    public async Task<List<T>> GetAllAsync(string tenantId, bool tracking = false)
    {
        if (tracking)
            return await _dbSet.Where(e => e.TenantId == tenantId).ToListAsync();
        return await _dbSet.AsNoTracking().Where(e => e.TenantId == tenantId).ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task DeleteByIdAsync(int id, string tenantId)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.TenantId == tenantId);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> predicate, string tenantId, bool tracking = false)
    {
        var query = _dbSet.Where(e => e.TenantId == tenantId).Where(predicate);
        if (!tracking)
        {
            query = query.AsNoTracking();
        }
        return await query.ToListAsync();
    }
}
