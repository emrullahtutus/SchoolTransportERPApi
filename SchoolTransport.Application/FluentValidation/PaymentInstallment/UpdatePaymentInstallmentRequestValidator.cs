using FluentValidation;
using SchoolTransport.Application.DTOs.PaymentInstallment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.FluentValidation.PaymentInstallment
{
    public class UpdatePaymentInstallmentRequestValidator : AbstractValidator<UpdatePaymentInstallmentRequest>
    {
        public UpdatePaymentInstallmentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID boş bırakılamaz.")
                .GreaterThan(0).WithMessage("ID 0'dan büyük olmalıdır.");

            RuleFor(x => x.InstallmentAmount)
                .NotEmpty().WithMessage("Taksit tutarı boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Taksit tutarı 0'dan büyük olmalıdır.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Son ödeme tarihi boş bırakılamaz.");
        }
    }
}
