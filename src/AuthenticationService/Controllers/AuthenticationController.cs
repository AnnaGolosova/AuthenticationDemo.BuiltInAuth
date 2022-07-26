using Demo.AuthService.Models;
using Demo.AuthService.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AuthService.Controllers
{
    /// <summary>
    /// Controller to handle Demo Authorization flow
    /// </summary>
    [Authorize]
	[Route("api/auth")]
	[ApiController]
	public class AuthenticationController : ControllerBase
    {
        private readonly IJWTRepository _jwtRepository;

        public AuthenticationController(IJWTRepository jwtRepository)
        {
			_jwtRepository = jwtRepository;
        }

		/// <summary>
		/// Login to the system
		/// </summary>
		/// <param name="user">User info to login</param>
		/// <returns>Generated tokens or error message</returns>
		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		[ProducesResponseType(typeof(Tokens), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status400BadRequest)]
		public IActionResult Authenticate(User user)
		{
			var token = _jwtRepository.Authenticate(user);

			if (token == null)
			{
				return Unauthorized($"Cannot authorize a user with login {user.Name}");
			}

			return Ok(token);
		}
	}
}
