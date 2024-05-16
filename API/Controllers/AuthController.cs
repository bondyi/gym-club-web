using API.Core.Dto;
using API.Core.Interfaces;
using API.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto request)
        {
            try
            {
                await _authService.RegisterAsync(request);
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e.Message));
            }

            return Ok(new Response("Registration successful."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            TokenPairDto tokens;

            try
            {
                tokens = await _authService.LoginAsync(request);
            }
            catch (NullReferenceException)
            {
                return NotFound(new Response("Not found."));
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e.Message));
            }

            return Ok(new AuthResponse("Login successful", tokens.AccessToken, tokens.RefreshToken));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens(string refreshToken)
        {
            TokenPairDto tokens;
            try
            {
                tokens = await _authService.RefreshTokensAsync(refreshToken);
            }
            catch (Exception e)
            {
                return BadRequest(new Response(e.Message));
            }

            return Ok(new AuthResponse("Refresh successful", tokens.AccessToken, tokens.RefreshToken));
        }
    }
}
