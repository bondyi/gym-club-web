using API.Core.Dto;

namespace API.Core.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto request);
        Task<TokenPairDto> LoginAsync(LoginDto request);
        Task<TokenPairDto> RefreshTokensAsync(string refreshToken);
    }
}
