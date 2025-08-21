using AutoMapper;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Expenses;
using SchoolTransport.Application.Exceptions.BadRequest;
using SchoolTransport.Application.Exceptions.NotFound;
using SchoolTransport.Application.Repositories;
using SchoolTransport.Domain.Entities.Expenses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolTransport.Application.Concrete
{
    public class ExpensesService : IExpensesService
    {
        private readonly IExpensesRepository _expensesRepository;
        private readonly IMapper _mapper;

        public ExpensesService(IExpensesRepository expensesRepository, IMapper mapper)
        {
            _expensesRepository = expensesRepository;
            _mapper = mapper;
        }

        public async Task<ExpenseResponseDto> CreateAsync(ExpenseRequestDto expenseDto, string tenantId)
        {
            if (expenseDto == null)
            {
                throw new ExpenseBadRequestException("Gider oluşturma isteği boş olamaz.");
            }

            var expense = _mapper.Map<Expense>(expenseDto);
            expense.TenantId = tenantId;

            await _expensesRepository.AddAsync(expense);
            await _expensesRepository.SaveChangesAsync();

            return _mapper.Map<ExpenseResponseDto>(expense);
        }

        public async Task<ExpenseResponseDto> UpdateAsync(int id, ExpenseRequestDto expenseDto, string tenantId)
        {
            if (expenseDto == null)
            {
                throw new ExpenseBadRequestException("Gider güncelleme isteği boş olamaz.");
            }

            var existingExpense = await _expensesRepository.GetByIdAsync(id, tenantId, true);
            if (existingExpense == null)
            {
                throw new ExpenseNotFoundException(id);
            }

            _mapper.Map(expenseDto, existingExpense);

            await _expensesRepository.UpdateAsync(existingExpense);
            await _expensesRepository.SaveChangesAsync();

            return _mapper.Map<ExpenseResponseDto>(existingExpense);
        }

        public async Task<ExpenseResponseDto> GetByIdAsync(int id, string tenantId)
        {
            var expense = await _expensesRepository.GetByIdAsync(id, tenantId);
            if (expense == null)
            {
                throw new ExpenseNotFoundException(id);
            }
            return _mapper.Map<ExpenseResponseDto>(expense);
        }

        public async Task<List<ExpenseResponseDto>> GetAllAsync(string tenantId)
        {
            var expenses = await _expensesRepository.GetAllAsync(tenantId);
            return _mapper.Map<List<ExpenseResponseDto>>(expenses);
        }

        public async Task DeleteAsync(int id, string tenantId)
        {
            var expense = await _expensesRepository.GetByIdAsync(id, tenantId);
            if (expense == null)
            {
                throw new ExpenseNotFoundException(id);
            }

            await _expensesRepository.DeleteByIdAsync(id, tenantId);
            await _expensesRepository.SaveChangesAsync();
        }

        public async Task<List<ExpenseResponseDto>> GetVehicleExpense(int vehicleId, string tenantId)
        {
            var expenses = await _expensesRepository.GetVehicleExpense(vehicleId, tenantId);
            return _mapper.Map<List<ExpenseResponseDto>>(expenses);
        }
    }
}