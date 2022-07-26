using Demo.AuthService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.AuthService.Controllers
{
    /// <summary>
    /// Sample controller to get dummy Users info
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Returns a collection of fake users
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseErrorResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult Get()
        {
            var users = new List<string>
            {
                "Alice",
                "Bob",
                "Hanna"
            };

            return Ok(users);
        }
    }
}
