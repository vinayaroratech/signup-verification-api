using FluentValidation;

namespace VA.Identity.Application.Features.Users.Commands
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(p => p.Email)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Password)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
        }
    }
}