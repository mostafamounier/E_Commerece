using AutoMapper;
using E_Commerce.Dtos;
using E_Commerce.Errors;
using E_Commerce.Extensions;
using E_Commerece.Core.Models.Identity;
using E_Commerece.Core.Services;
using E_Commerece.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{

    public class AccountController : ApiControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            this.mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
                if (ChechkEmailExists(model.Email).Result.Value) 
                    return BadRequest( new ValidationErrorResponse() { Errors = new string[] { "this email  exists" } });
                

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
                    Token = await _tokenService.CreateTokenAsync(user)
                });


        }

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
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser() {

            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync((string)Email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user)
            });

        }
        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress(){

            var user =  _userManager.GetUserAddress(User);
            Address address = user.Address;
            AddressDto addressDto =mapper.Map<AddressDto>(address);
            return Ok(addressDto);

        }
        [Authorize]
        [HttpPut("UpdateAddress")]
        public async Task<ActionResult<AddressDto>> UpdatUserAddress(AddressDto UpdateAdrress)
        {
            var user = _userManager.GetUserAddress(User);
            
            var Address = mapper.Map<Address>(user.Address);

            user.Address = Address;

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded) 
                return BadRequest(new ApiErrorResponse(400));
            return Ok(UpdateAdrress);

        }
        [HttpGet]
        public async Task<ActionResult<bool>> ChechkEmailExists(string email) { 
        
            return await _userManager.FindByEmailAsync(email) is not null ;
        }

    }
}
