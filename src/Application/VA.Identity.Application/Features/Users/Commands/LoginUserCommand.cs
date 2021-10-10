using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using VA.Identity.Application.Common.Models;
using VA.Identity.Application.Jwt;
using VA.Identity.Application.Jwt.Model;

namespace VA.Identity.Application.Features.Users.Commands
{
    public class LoginUserCommand : IRequest<Result<UserResponse<string>>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UpdateUserCommandHandler : IRequestHandler<LoginUserCommand, Result<UserResponse<string>>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;

        public UpdateUserCommandHandler(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appJwtSettings = appJwtSettings.Value;
        }


        public async Task<Result<UserResponse<string>>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (result.Succeeded)
            {
                return Result<UserResponse<string>>.Success(GetUserResponse(request.Email));
            }

            if (result.IsLockedOut)
            {
                return Result<UserResponse<string>>.Failure(null, new string[] { "This user is blocked" });
            }

            return Result<UserResponse<string>>.Failure(null, new string[] { "Incorrect user or password" });
        }

        private UserResponse<string> GetUserResponse(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildUserResponse();
        }
    }
}