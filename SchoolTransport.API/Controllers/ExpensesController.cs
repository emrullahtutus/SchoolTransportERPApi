using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Expenses;
using FluentValidation;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace SchoolTransport.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Admin")]
   
    public class ExpensesController : BaseController
    {
        private readonly IExpensesService _expensesService;
        private readonly IValidator<ExpenseRequestDto> _validator;
        private readonly IMapper _mapper;

        public ExpensesController(IExpensesService expensesService, IValidator<ExpenseRequestDto> validator, IMapper mapper)
        {
            _expensesService = expensesService;
            _validator = validator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ExpenseResponseDto>> Create([FromBody] ExpenseRequestDto expenseDto)
        {
            var validationResult = await _validator.ValidateAsync(expenseDto);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            var result = await _expensesService.CreateAsync(expenseDto, tenantId);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ExpenseResponseDto>> Update(int id, [FromBody] ExpenseRequestDto expenseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tenantId = GetTenantId();
            var result = await _expensesService.UpdateAsync(id, expenseDto, tenantId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ExpenseResponseDto>>> GetAll()
        {
            var tenantId = GetTenantId();
            var result = await _expensesService.GetAllAsync(tenantId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var tenantId = GetTenantId();
            await _expensesService.DeleteAsync(id, tenantId);
            return NoContent();
        }

        [HttpGet("{vehicleId:int}")]
        public async Task<IActionResult> GetVehicleExpense(int vehicleId)
        {
            var tenantId = GetTenantId();
            var expenseList = await _expensesService.GetVehicleExpense(vehicleId, tenantId);
            return Ok(expenseList);
        }
    }
}