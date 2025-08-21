using FluentValidation;
using SchoolTransport.Application.DTOs.School;

namespace SchoolTransport.Application.Validators.School
{
    public class CreateSchoolRequestValidator : AbstractValidator<CreateSchoolRequest>
    {
        public CreateSchoolRequestValidator()
        {
            RuleFor(x => x.Name)
     .NotNull()
     .WithMessage("Okul adı gereklidir")
     .NotEmpty()
     .WithMessage("Okul adı boş olamaz")
     .MinimumLength(3)
     .WithMessage("Okul adı minimum 3 karakter olmalıdır")
     .MaximumLength(200)
     .WithMessage("Okul adı maksimum 200 karakter olabilir");
        }
    }
}
