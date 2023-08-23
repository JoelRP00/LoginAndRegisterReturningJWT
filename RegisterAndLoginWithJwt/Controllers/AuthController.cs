using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegisterAndLoginWithJwt.DTO;
using RegisterAndLoginWithJwt.Model;
using RegisterAndLoginWithJwt.Services.Interface;

namespace RegisterAndLoginWithJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("Registro")]
        public async Task<ActionResult<User>> Cadastrar([FromBody] UserDTO userDTO)
        {
            var user = await _userServices.Register(userDTO);
            if(ModelState.IsValid)
            {
                return Ok(user);
            }
            return BadRequest();
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Logar([FromBody] UserDTO userDTO)
        {
            var result = await _userServices.Login(userDTO);
            if(ModelState.IsValid)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
