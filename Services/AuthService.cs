using DTO.User;
using Repositories;


namespace Services
{
	public class AuthService
	{
		private readonly AuthRepository _authRepository;
		public AuthService(AuthRepository authRepository)
		{
			this._authRepository = authRepository;
		}

		/// <summary>
		/// registration of a new user
		/// </summary>
		/// <param name="registerDTO"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task RegisterAsync(CreateUserDTO createUserDTO)
		{
			if (createUserDTO.Password == null || createUserDTO.Password != createUserDTO.ConfirmPassword)
			{
				throw new Exception("Le mot de passe et la confirmation du mot de passe doivent être identiques !");
			}
			if (createUserDTO.EmailLogin == null || createUserDTO.EmailLogin == "" || createUserDTO.EmailLogin.Length < 3)
			{
				throw new Exception("Le login est incorrect !");
			}
			await this._authRepository.RegisterAdminAsync(createUserDTO);
		}
	}
}
