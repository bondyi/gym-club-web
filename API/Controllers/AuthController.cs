using API.Core.Dto;
using API.Core.Interfaces;
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
                return BadRequest(e.Message);
            }

            return Ok();
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
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(new {tokens.AccessToken, tokens.RefreshToken});
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
                return BadRequest(e.Message);
            }

            return Ok(new { tokens.AccessToken, tokens.RefreshToken });
        }
    }
}
