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

            if (!Regex.IsMatch(dto.PhoneNumber, RegexPatterns.PHONE_NUMBER) &&
                !Regex.IsMatch(dto.Password, RegexPatterns.PASSWORD) &&
                !Regex.IsMatch(dto.Name, RegexPatterns.NAME) &&
                !Regex.IsMatch(dto.Surname, RegexPatterns.SURNAME) &&
                !Regex.IsMatch(dto.BirthDate.ToString()!, RegexPatterns.DATE))
            {
                throw new Exception("Invalid data.");
            }

            if (await _repository.GetUserByPhoneNumber(dto.PhoneNumber) != null)
            {
                throw new Exception("User already exists.");
            }

            var newUser = new User()
            {
                UserRole = dto.UserRole,
                PhoneNumber = dto.PhoneNumber,
                HashPassword = _passwordHasher.HashPassword(dto, dto.Password),
                Name = dto.Name,
                Surname = dto.Surname,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender
            };

            await _repository.Post(newUser);
        }

        public async Task<TokenPairDto> LoginAsync(LoginDto dto)
        {
            if (!Regex.IsMatch(dto.PhoneNumber, RegexPatterns.PHONE_NUMBER) &&
                !Regex.IsMatch(dto.Password, RegexPatterns.PASSWORD))
            {
                throw new Exception("Invalid data.");
            }

            var user = await _repository.GetUserByPhoneNumber(dto.PhoneNumber) ?? throw new NullReferenceException();

            if (_passwordHasher.VerifyHashedPassword(dto, user.HashPassword, dto.Password) == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }

            var accessToken = _tokenHelper.GenerateAccessToken(user);

            if (user.RefreshTokenCreatedAt == null || user.RefreshTokenCreatedAt.Value.AddMonths(1) < DateTime.UtcNow)
            {
                user.RefreshToken = _tokenHelper.GenerateRefreshToken();
                user.RefreshTokenCreatedAt = DateTime.UtcNow;

                await _repository.Put(user.UserId, user);
            }

            return new TokenPairDto
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken!
            };
        }

        public async Task<TokenPairDto> RefreshTokensAsync(string refreshToken)
        {
            var user = await _repository.GetUserByRefreshToken(refreshToken) ?? throw new NullReferenceException();

            if (user.RefreshTokenCreatedAt == null) throw new NullReferenceException();

            if (user.RefreshTokenCreatedAt.Value.AddMonths(1) < DateTime.UtcNow)
            {
                throw new Exception("The token has expired.");
            }

            var accessToken = _tokenHelper.GenerateAccessToken(user);
            
            if (user.RefreshToken == null)
            {
                user.RefreshToken = _tokenHelper.GenerateRefreshToken();
                user.RefreshTokenCreatedAt = DateTime.UtcNow;
            }

            await _repository.Put(user.UserId, user);

            return new TokenPairDto
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken
            };
        }
    }
}
