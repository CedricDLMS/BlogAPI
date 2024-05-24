using DTO.User;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class AuthRepository
	{
		private readonly BlogDBContext _context;
		private readonly UserManager<AppUser> _userManager;
		public AuthRepository(BlogDBContext context, UserManager<AppUser> userManager)
		{
			this._context = context;
			this._userManager = userManager;
		}

		/// <summary>
		///  registration of a new user
		/// </summary>
		/// <param name="createUserDTO"></param>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public async Task RegisterAdminAsync(CreateUserDTO createUserDTO)
		{
			DateTable date = new DateTable
			{
				JjMmYyyy = DateOnly.FromDateTime(DateTime.Now)
			};
			await this._context.DateTables.AddAsync(date);
			AppUser appUser = new AppUser
			{
				UserName = createUserDTO.EmailLogin.ToUpper(),
				NormalizedUserName = createUserDTO.EmailLogin.ToUpper(),
				Email = createUserDTO.EmailLogin,
				NormalizedEmail = createUserDTO.EmailLogin.ToUpper()
			};

			IdentityResult? identityResult = await this._userManager.CreateAsync(appUser, createUserDTO.Password);

			if (identityResult.Succeeded)
			{
				await this._userManager.AddToRoleAsync(appUser, "ADMIN");
				await this._context.Utilisateurs.AddAsync(
					new Utilisateur
					{
						Id = appUser.Id,
						Prenom = createUserDTO.Firstname,
						Nom = createUserDTO.Lastname,
						Pseudo = createUserDTO.Pseudo,
						AppUserId = appUser.Id,
						DateTableId = date.Id,
						Dates = date
					});
				await this._context.SaveChangesAsync();
			}
			else
			{
				throw new Exception(identityResult.Errors.ToString());
			}
		}
	}
}
