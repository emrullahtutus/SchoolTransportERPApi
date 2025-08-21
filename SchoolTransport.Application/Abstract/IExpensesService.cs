using SchoolTransport.Application.DTOs.Expenses;
using SchoolTransport.Domain.Common;
using SchoolTransport.Domain.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Abstract
{

    public interface IExpensesService
    {
        Task<ExpenseResponseDto> CreateAsync(ExpenseRequestDto expenseDto, string tenantId);
        Task<ExpenseResponseDto> UpdateAsync(int id, ExpenseRequestDto expenseDto, string tenantId);
        Task<ExpenseResponseDto> GetByIdAsync(int id, string tenantId);
        Task<List<ExpenseResponseDto>> GetAllAsync(string tenantId);
        Task DeleteAsync(int id, string tenantId);
        Task<List<ExpenseResponseDto>> GetVehicleExpense(int vehicleId, string tenantId);
    }

}
