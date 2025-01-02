using FluentValidation;
using SkyWheels.Contact.API.Dtos;

namespace SkyWheels.Contact.API.ValidationRules
{
    public class UpdateContactValidator:AbstractValidator<UpdateContactDto>
    {
        public UpdateContactValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Ad alanı en az 2 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Ad alanı en fazla 20 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Soyad alanı en az 2 karakter olmalıdır.")
                .MaximumLength(25).WithMessage("Soyad alanı en fazla 25 karakter olabilir.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon Numarası alanı boş geçilemez.")
                .Length(11).WithMessage("Telefon Numarası alanı en az 11 karakter olmalıdır.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj alanı boş geçilemez.")
                .MinimumLength(2).WithMessage("Mesaj alanı en az 10 karakter olmalıdır.")
                .MaximumLength(25).WithMessage("Mesaj alanı en fazla 100 karakter olabilir.");
        }
    }
}
