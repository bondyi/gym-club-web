using API.Core.Dto;
using API.Core.Models.Responses;

namespace API.Core.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterDto request);
        Task<TokenPairDto> LoginAsync(LoginDto request);
        Task<TokenPairDto> RefreshTokensAsync(string refreshToken);
    }
}
