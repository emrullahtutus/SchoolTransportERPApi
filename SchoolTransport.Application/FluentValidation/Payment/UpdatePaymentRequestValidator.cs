using FluentValidation;
using SchoolTransport.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.FluentValidation.Payment
{
    public class UpdatePaymentRequestValidator : AbstractValidator<UpdatePaymentRequest>
    {
        public UpdatePaymentRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("ID boş bırakılamaz.")
                .GreaterThan(0).WithMessage("ID 0'dan büyük olmalıdır.");

            RuleFor(x => x.TotalFee)
                .NotEmpty().WithMessage("Toplam ücret boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Toplam ücret 0'dan büyük olmalıdır.");
        }
    }
}
