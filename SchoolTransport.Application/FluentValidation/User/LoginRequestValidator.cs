using FluentValidation;
using SchoolTransport.Application.DTOs.User;

namespace SchoolTransport.Application.Validators.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .WithMessage("Username or email is required")
                .MaximumLength(256)
                .WithMessage("Username or email cannot exceed 256 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters");

        }
    }
}