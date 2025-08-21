using FluentValidation;
using SchoolTransport.Application.DTOs.School;

namespace SchoolTransport.Application.Validators.School
{
    public class UpdateSchoolRequestValidator : AbstractValidator<UpdateSchoolRequest>
    {
        public UpdateSchoolRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Okul adı gereklidir")
                .MaximumLength(200)
                .WithMessage("Okul adı maksimum 200 karakter olabilir")
                .MinimumLength(3)
                .WithMessage("Okul adı minimum 3 karakter olmalıdır");
        }
    }
}
