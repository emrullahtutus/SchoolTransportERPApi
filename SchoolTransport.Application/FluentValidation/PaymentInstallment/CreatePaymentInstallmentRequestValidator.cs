using FluentValidation;
using SchoolTransport.Application.DTOs.PaymentInstallment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.FluentValidation.PaymentInstallment
{
    public class CreatePaymentInstallmentRequestValidator : AbstractValidator<CreatePaymentInstallmentRequest>
    {
        public CreatePaymentInstallmentRequestValidator()
        {
            RuleFor(x => x.PaymentId)
                .NotEmpty().WithMessage("Ödeme kimliği boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Ödeme kimliği 0'dan büyük olmalıdır.");

            RuleFor(x => x.MonthNumber)
                .NotEmpty().WithMessage("Ay numarası boş bırakılamaz.")
                .InclusiveBetween(1, 12).WithMessage("Ay numarası 1 ile 12 arasında olmalıdır.");

            RuleFor(x => x.MonthName)
                .NotEmpty().WithMessage("Ay adı boş bırakılamaz.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Son ödeme tarihi boş bırakılamaz.");

            RuleFor(x => x.InstallmentAmount)
                .NotEmpty().WithMessage("Taksit tutarı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Taksit tutarı 0'dan büyük olmalıdır.");
        }
    }
}
