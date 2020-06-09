using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Automapper_JWTTokens_Demo.Data;
using Automapper_JWTTokens_Demo.Dtos.User;
using Automapper_JWTTokens_Demo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Automapper_JWTTokens_Demo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            this._authRepository = authRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register (UserRegisterDto request)
        {
            ServiceResponse<int> serviceResponse = await _authRepository.Regsiter(
                new User { Username = request.Username }, request.password);
            if(!serviceResponse.Success)
            {
                return BadRequest(serviceResponse);
            }
            return Ok(serviceResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login (UserRegisterDto request)
        {
            ServiceResponse<string> response = await _authRepository.Login(
                request.Username, request.password);

            if(!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}