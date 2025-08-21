using FluentValidation;
using SchoolTransport.Application.DTOs.Driver;

namespace SchoolTransport.Application.Validators.Driver
{
    public class UpdateDriverRequestValidator : AbstractValidator<UpdateDriverRequest>
    {
        public UpdateDriverRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Şoför adı gereklidir")
                .MaximumLength(200)
                .WithMessage("Şoför adı maksimum 200 karakter olabilir")
                .MinimumLength(2)
                .WithMessage("Şoför adı minimum 2 karakter olmalıdır")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")
                .WithMessage("Şoför adı sadece harf ve boşluk içerebilir");
        }
    }
}
