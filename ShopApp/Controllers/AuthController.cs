using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DTOs;
using ShopApp.Repositories;

namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        CustomReturnDTO customReturnDTO;
        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> PostRegister(RegisterUserDTO registerUserDTO)
        {
            customReturnDTO=await _userRepository.PostRegister(registerUserDTO);
            if (customReturnDTO.Type == "BAD")
                return BadRequest(customReturnDTO.Message);
                return Ok(customReturnDTO.Message);
        }
        [HttpPost("login")]
        public async Task<IActionResult> PostLogin(LoginUserDTO loginUserDTO)
        {
            customReturnDTO = await _userRepository.PostLogin(loginUserDTO);
            if (customReturnDTO.Type == "BAD")
                return BadRequest(customReturnDTO.Message);
                return Ok(customReturnDTO.Data);
        }
    }
}
