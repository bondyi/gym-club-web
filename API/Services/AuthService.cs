using API.Core.Dto;
using API.Core.Interfaces;
using API.Core.Models;
using API.Core.Models.Entities;
using API.Helpers;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace API.Services
{
    public class AuthService(GymClubContext context, IConfiguration configuration) : IAuthService
    {
        private readonly UserRepository _repository = new(context);
        private readonly PasswordHasher<LoginDto> _passwordHasher = new();
        private readonly TokenHelper _tokenHelper = new(configuration);

        public async Task RegisterAsync(RegisterDto dto)
        {
            if (Regex.IsMatch(dto.PhoneNumber, RegexPatterns.PHONE_NUMBER) ||
                Regex.IsMatch(dto.Password, RegexPatterns.PASSWORD))
            {
                throw new Exception("Wrong format.");
            }

            if (await _repository.GetUserByPhoneNumber(dto.PhoneNumber) != null)
            {
                throw new Exception("User already exists.");
            }

            var newUser = new User()
            {
                UserRole = dto.UserRole,
                PhoneNumber = dto.PhoneNumber,
                HashPassword = _passwordHasher.HashPassword(dto, dto.Password)
            };

            await _repository.Post(newUser);
        }

        public async Task<TokenPairDto> LoginAsync(LoginDto dto)
        {
            if (Regex.IsMatch(dto.PhoneNumber, RegexPatterns.PHONE_NUMBER) ||
                Regex.IsMatch(dto.Password, RegexPatterns.PASSWORD))
            {
                throw new Exception("Wrong format.");
            }

            var user = await _repository.GetUserByPhoneNumber(dto.PhoneNumber) ?? throw new NullReferenceException();

            if (_passwordHasher.VerifyHashedPassword(dto, user.HashPassword, dto.Password) == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }

            var accessToken = _tokenHelper.GenerateAccessToken(user);

            if (user.RefreshToken == null || user.RefreshTokenCreatedAt.Value.AddMonths(1) > DateTime.UtcNow)
            {
                user.RefreshToken = _tokenHelper.GenerateRefreshToken();
                user.RefreshTokenCreatedAt = DateTime.UtcNow;

                await _repository.Put(user, user.UserId);
            }

            return new TokenPairDto(accessToken, user.RefreshToken);
        }

        public async Task<TokenPairDto> RefreshTokensAsync(string refreshToken)
        {
            var user = await _repository.GetUserByRefreshToken(refreshToken);

            if (user.RefreshToken != refreshToken)
            {
                throw new Exception("Invalid refresh token.");
            }

            if (user.RefreshTokenCreatedAt.Value.AddMonths(1) > DateTime.UtcNow)
            {
                throw new Exception("The token has expired.");
            }

            var accessToken = _tokenHelper.GenerateAccessToken(user);
            var newRefreshToken = _tokenHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenCreatedAt = DateTime.UtcNow;

            await _repository.Put(user, user.UserId);

            return new TokenPairDto(accessToken, refreshToken);
        }
    }
}
