using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using VA.Identity.Application.Common.Models;
using VA.Identity.Application.Jwt;
using VA.Identity.Application.Jwt.Model;

namespace VA.Identity.Application.Features.Users.Commands
{
    public partial class RegisterUserCommand : IRequest<Result<UserResponse<string>>>
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }

    public class CreateUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserResponse<string>>>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppJwtSettings _appJwtSettings;

        public CreateUserCommandHandler(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            IOptions<AppJwtSettings> appJwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _appJwtSettings = appJwtSettings.Value;
        }

        public async Task<Result<UserResponse<string>>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            IdentityUser user = new()
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return Result<UserResponse<string>>.Success(GetUserResponse(user.Email));
            }


            IList<string> errors = new List<string>();
            foreach (IdentityError error in result.Errors)
            {
                errors.Add(error.Description);
            }


            return Result<UserResponse<string>>.Failure(null, errors);
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

        private string GetFullJwt(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithJwtClaims()
                .WithUserClaims()
                .WithUserRoles()
                .BuildToken();
        }

        private string GetJwtWithoutClaims(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .BuildToken();
        }

        private string GetJwtWithUserClaims(string email)
        {
            return new JwtBuilder()
                .WithUserManager(_userManager)
                .WithJwtSettings(_appJwtSettings)
                .WithEmail(email)
                .WithUserClaims()
                .BuildToken();
        }
    }

}
