using DTO.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly AuthService _authService;
		public AuthController(AuthService authService)
		{
			this._authService = authService;
		}

		/// <summary>
		/// registration of a new user
		/// </summary>
		/// <param name="createUserDTO"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> Register(CreateUserDTO createUserDTO)
		{
			try
			{
				await this._authService.RegisterAsync(createUserDTO);
				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
