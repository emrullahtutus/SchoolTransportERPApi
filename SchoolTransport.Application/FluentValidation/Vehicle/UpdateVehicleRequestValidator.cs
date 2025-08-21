using FluentValidation;
using SchoolTransport.Application.DTOs.Vehicle;

namespace SchoolTransport.Application.Validators.Vehicle
{
    public class UpdateVehicleRequestValidator : AbstractValidator<UpdateVehicleRequest>
    {
        public UpdateVehicleRequestValidator()
        {
            RuleFor(x => x.PlateNumber)
                .NotEmpty()
                .WithMessage("Araç plakası gereklidir")
                .MaximumLength(20)
                .WithMessage("Araç plakası maksimum 20 karakter olabilir")
                .Matches(@"^[0-9]{2}\s?[A-ZÇĞıİÖŞÜ]{1,3}\s?[0-9]{1,4}$")
                .WithMessage("Geçerli bir Türkiye araç plakası giriniz (örn: 34 ABC 1234)")
                .Must(BeValidTurkishPlate)
                .WithMessage("Geçerli bir Türkiye il koduna sahip plaka giriniz");

            RuleFor(x => x.DriverId)
                .GreaterThan(0)
                .When(x => x.DriverId.HasValue)
                .WithMessage("Geçerli bir şoför seçiniz");

            RuleFor(x => x.SchoolIds)
                .Must(schoolIds => schoolIds == null || schoolIds.All(id => id > 0))
                .WithMessage("Geçerli okul ID'leri giriniz")
                .Must(schoolIds => schoolIds == null || schoolIds.Distinct().Count() == schoolIds.Count())
                .WithMessage("Aynı okul birden fazla kez seçilemez");
        }

        private bool BeValidTurkishPlate(string plateNumber)
        {
            if (string.IsNullOrEmpty(plateNumber)) return false;

            var cleanPlate = plateNumber.Replace(" ", "");
            if (cleanPlate.Length < 5) return false;

            var cityCode = cleanPlate.Substring(0, 2);
            return int.TryParse(cityCode, out int code) && code >= 1 && code <= 81;
        }
    }
}

