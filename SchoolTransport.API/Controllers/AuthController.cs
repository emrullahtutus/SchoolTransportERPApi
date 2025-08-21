using Microsoft.AspNetCore.Mvc;
using SchoolTransport.Application.Abstract;
using SchoolTransport.Application.DTOs.Token;
using SchoolTransport.Application.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace SchoolTransport.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Token token = await _authService.LoginAsync(
                    request.UsernameOrEmail,
                    request.Password,
                  request.AccessTokenLifeTime.GetValueOrDefault());
                  

                if (token == null)
                    return Unauthorized(new { message = "Invalid username/email or password" });

                return Ok(new LoginResponse
                {
                    Token = token.AccessToken,
                    Expiration = token.Expiration,
                    Success = true,
                    Message = "Login successful"
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login" });
            }
        }


    }

    }