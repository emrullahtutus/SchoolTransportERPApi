using FluentValidation;
using SchoolTransport.Application.DTOs.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.FluentValidation.Expenses
{
    public class ExpenseRequestDtoValidator : AbstractValidator<ExpenseRequestDto>
    {
        public ExpenseRequestDtoValidator()
        {
            // DateTime validation - Son 1 ay içinde, gelecek tarih olamaz
            RuleFor(x => x.DateTime)
                .GreaterThanOrEqualTo(DateTime.Now.AddMonths(-1))
                .WithMessage("Masraf tarihi son 1 ay içinde olmalı")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Masraf tarihi gelecekte olamaz");

            // Decimal alanlar - Negatif olamaz
            RuleFor(x => x.Fuel)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Yakıt masrafı negatif olamaz")
                .When(x => x.Fuel.HasValue);

            RuleFor(x => x.Penalty)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Ceza masrafı negatif olamaz")
                .When(x => x.Penalty.HasValue);

            RuleFor(x => x.Insurance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Sigorta masrafı negatif olamaz")
                .When(x => x.Insurance.HasValue);

            RuleFor(x => x.Industry)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Sanayi masrafı negatif olamaz")
                .When(x => x.Industry.HasValue);

            RuleFor(x => x.Salary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Maaş masrafı negatif olamaz")
                .When(x => x.Salary.HasValue);

            // En az bir masraf alanı dolu olmalı
            RuleFor(x => x)
                .Must(HaveAtLeastOneExpense)
                .WithMessage("En az bir masraf alanı dolu olmalı");

            RuleFor(x => x.VehicleId)
                .NotEmpty();
        }
        private bool HaveAtLeastOneExpense(ExpenseRequestDto dto)
        {
            return dto.Fuel.HasValue && dto.Fuel > 0 ||
                   dto.Penalty.HasValue && dto.Penalty > 0 ||
                   dto.Insurance.HasValue && dto.Insurance > 0 ||
                   dto.Industry.HasValue && dto.Industry > 0 ||
                   dto.Salary.HasValue && dto.Salary > 0;
        }
    }
    }
