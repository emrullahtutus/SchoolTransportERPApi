using SchoolTransport.Domain.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Repositories
{
    public interface IExpensesRepository : IRepositoryBase<Expense>
    {
        Task<List<Expense>> GetVehicleExpense(int vehicleId, string tenantId);
    }
}
