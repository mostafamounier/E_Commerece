using E_Commerce.Errors;
using E_Commerece.Core.Models.Identity;
using E_Commerece.Core.Repositories;
using E_Commerece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{

    public class AccountController : ApiControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Email,
                DisplayName = model.DisplayName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new ApiErrorResponse(400));

            return Ok(
                new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token =await _tokenService.CreateTokenAsync(user)
                });


        }

        // ✅ Login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("Invalid Email");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!result)
                return Unauthorized(new ApiErrorResponse(401));

            return Ok(
                new UserDto()
                {
                    Email = user.Email,
                    DisplayName = user.DisplayName,
                    Token = await _tokenService.CreateTokenAsync(user)

                });
        }
    }
}
