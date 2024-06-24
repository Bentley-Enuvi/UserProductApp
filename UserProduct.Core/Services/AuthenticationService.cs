using Microsoft.AspNetCore.Identity;
using UserProduct.Core.Abstractions;
using UserProduct.Core.DTOs;
using UserProduct.Domain.Entities;

namespace UserProduct.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtGenerator _jwtService;
        private readonly UserManager<User> _userManager;

        public AuthenticationService(UserManager<User> userManager, IJwtGeneratorService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<Result> Register(RegRequestDto registerDto)
        {
            var user = User.Create(registerDto.FirstName, registerDto.LastName, registerDto.Email);

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
                return result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            result = await _userManager.AddToRoleAsync(user, RolesConstant.User);
            if (!result.Succeeded)
                return result.Errors.Select(error => new Error(error.Code, error.Description)).ToArray();

            return Result.Success(user);
        }



        public async Task<Result<LoginResponseDto>> Login(LoginDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Email);

            if (user is null) return new Error[] { new("Auth.Error", "email or password not correct") };

            var isValidUser = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);

            if (!isValidUser) return new Error[] { new("Auth.Error", "email or password not correct") };

            var token = _jwtService.GenerateToken(user);

            return new LoginResponseDto(token);
        }
    }
}
