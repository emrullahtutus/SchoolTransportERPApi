using FluentValidation;
using SchoolTransport.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTransport.Application.FluentValidation.Payment
{
    public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
    {
        public CreatePaymentRequestValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Öğrenci kimliği boş bırakılamaz.")
                .GreaterThan(0).WithMessage("Öğrenci kimliği 0'dan büyük olmalıdır.");

        }
    }
}
