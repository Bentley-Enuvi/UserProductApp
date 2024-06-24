using UserProduct.Core.DTOs;

namespace UserProduct.Core.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<Result> Register(RegRequestDto regDto);

        public Task<Result<LoginResponseDto>> Login(LoginDto loginDto);
    }
}
