using SchoolTransport.Application.Repositories;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class BulkOperationRepository : IBulkOperationRepository
    {
        private readonly SchoolTransportDbContext _context;

        public BulkOperationRepository(SchoolTransportDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class
        {
            if (!entities.Any())
                return;

            // TenantId property kontrolü ve ataması
            foreach (var entity in entities)
            {
                SetTenantId(entity, tenantId);
            }

            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class
        {
            if (!entities.Any())
                return;

            // TenantId güvenlik kontrolü
            foreach (var entity in entities)
            {
                ValidateTenantId(entity, tenantId);
            }

            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync<T>(IEnumerable<T> entities, string tenantId) where T : class
        {
            if (!entities.Any())
                return;

            // TenantId güvenlik kontrolü
            foreach (var entity in entities)
            {
                ValidateTenantId(entity, tenantId);
            }

            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        private void SetTenantId<T>(T entity, string tenantId) where T : class
        {
            var tenantProperty = GetTenantIdProperty<T>();
            if (tenantProperty == null)
                return;

            var currentTenantId = tenantProperty.GetValue(entity) as string;

            if (string.IsNullOrEmpty(currentTenantId))
            {
                tenantProperty.SetValue(entity, tenantId);
            }
            else if (currentTenantId != tenantId)
            {
                throw new UnauthorizedAccessException($"Tenant mismatch: Expected {tenantId}, got {currentTenantId}");
            }
        }

        private void ValidateTenantId<T>(T entity, string tenantId) where T : class
        {
            var tenantProperty = GetTenantIdProperty<T>();
            if (tenantProperty == null)
                return;

            var currentTenantId = tenantProperty.GetValue(entity) as string;

            if (currentTenantId != tenantId)
            {
                throw new UnauthorizedAccessException($"Tenant mismatch: Expected {tenantId}, got {currentTenantId}");
            }
        }

        private PropertyInfo GetTenantIdProperty<T>() where T : class
        {
            return typeof(T).GetProperty("TenantId", BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
