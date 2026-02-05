using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(Core.DTO.LoginRequest loginRequest)
        {
            if (loginRequest == null) return BadRequest("Invalid login data");
            AuthenticationResponse? authResponse = await _userService.Login(loginRequest);
            if (authResponse == null || !authResponse.Success)
            {
                return Unauthorized(authResponse);
            }
            return Ok(authResponse);
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult>  Register(Core.DTO.RegisterRequest registerRequest )
        {
            if(registerRequest ==null) return BadRequest("Invalid registration data");
            AuthenticationResponse? authResponse = await _userService.Registration(registerRequest);
            if(authResponse == null || !authResponse.Success)
            {
                return BadRequest(authResponse); 
            }
            return Ok(authResponse);
        }
    }
}
