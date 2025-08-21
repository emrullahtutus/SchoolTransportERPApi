using Microsoft.EntityFrameworkCore;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Expenses;
using SchoolTransport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Persistence.Repositories
{
    public class ExpensesRepository : RepositoryBase<Expense>, IExpensesRepository
    {
        private readonly SchoolTransportDbContext _context;

        public ExpensesRepository(SchoolTransportDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetVehicleExpense(int vehicleId, string tenantId)
        {
            var expense = await _context.Expenses
                .Where(v => v.VehicleId == vehicleId && v.TenantId == tenantId)
                .ToListAsync();
            return expense;
        }
    }

}
