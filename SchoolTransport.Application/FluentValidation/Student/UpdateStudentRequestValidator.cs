using FluentValidation;
using SchoolTransport.Application.DTOs.Student;

namespace SchoolTransport.Application.FluentValidation.Student
{
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Öğrenci adı soyadı gereklidir")
                .MaximumLength(200)
                .WithMessage("Öğrenci adı soyadı maksimum 200 karakter olabilir")
                .Matches(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$")
                .WithMessage("Öğrenci adı soyadı sadece harf ve boşluk içerebilir");

            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty()
            //    .WithMessage("Telefon numarası gereklidir")
            //    .Matches(@"^(\+90|0)?[1-9][0-9]{9}$")
            //    .WithMessage("Geçerli bir Türkiye telefon numarası giriniz (örn: 05551234567)");

            RuleFor(x => x.SchoolId)
                .GreaterThan(0)
                .WithMessage("Geçerli bir okul seçiniz");
        }
    }
}

