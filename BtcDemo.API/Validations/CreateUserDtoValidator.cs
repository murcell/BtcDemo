using BtcDemo.Core.DTOs;
using FluentValidation;

namespace BtcDemo.API.Validations;

public class CreateUserDtoValidator : AbstractValidator<CreateAppUserDto>
{
	public CreateUserDtoValidator()
	{
		RuleFor(x => x.Email).NotEmpty().WithMessage("Email gereklidir").EmailAddress().WithMessage("Email yanlıştır.");
		RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir");
		RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı gereklidir.");
	}
}
