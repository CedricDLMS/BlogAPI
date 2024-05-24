using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Models;
using Repositories;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Main
{
    public class InitializeUser
    {
        //sdqdqsdqsd
        public async static Task adminInit(BlogDBContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if ((await roleManager.FindByNameAsync("ADMIN")) == null) // creer un role admin si null
            {
				DateTable date = new DateTable
				{
					JjMmYyyy = DateOnly.FromDateTime(DateTime.Now)
				};
				await context.DateTables.AddAsync(date);


				string adminPassword = "Azerty1+";
                AppUser userAdmin = new AppUser { Email = "admin@admin.fr", NormalizedEmail = "admin@admin.fr", UserName = "admin@admin.fr" };
                IdentityRole roleAdmin = new IdentityRole { Name = "admin", NormalizedName = "ADMIN" };

                Utilisateur userClass = new Utilisateur { Id= userAdmin.Id , AppUserId = userAdmin.Id, Prenom = "admin", Nom="ADMIN", AppUser = userAdmin, DateTableId = date.Id, Dates=date };

                IdentityRole? roleAdminCheck = await context.Roles.FindAsync(roleAdmin.Id);
                Utilisateur? userAdminCheck = await context.Utilisateurs.FindAsync(userClass.Id);

                if (roleAdminCheck == null && userAdminCheck == null)
                {
                    await userManager.CreateAsync(userAdmin, adminPassword);
                    await roleManager.CreateAsync(roleAdmin);

                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    await context.AddAsync(userClass);
                    await context.SaveChangesAsync();
                }
            }
            
            
        }
        public async static Task UserInit(BlogDBContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if ((await roleManager.FindByNameAsync("USER")) == null) // Creer un role user si null
            {
				DateTable date = new DateTable
				{
					JjMmYyyy = DateOnly.FromDateTime(DateTime.Now)
				};
				await context.DateTables.AddAsync(date);

				string userPassword = "Azerty1+";
                AppUser newUser = new AppUser { Email = "user@user.fr", NormalizedEmail = "user@user.fr", UserName = "user@user.fr" };
                IdentityRole roleUser = new IdentityRole { Name = "user", NormalizedName = "USER" };

				Utilisateur userClass = new Utilisateur { Id = newUser.Id, AppUserId = newUser.Id, Prenom = "user", Nom = "USER", DateTableId = date.Id, AppUser = newUser, Dates = date };

				IdentityRole? roleUserCheck = await context.Roles.FindAsync(roleUser.Id);
                Utilisateur? newUserCheck = await context.Utilisateurs.FindAsync(userClass.Id);

                if (roleUserCheck == null && newUserCheck == null)
                {
                    await userManager.CreateAsync(newUser, userPassword);
                    await roleManager.CreateAsync(roleUser);

                    await userManager.AddToRoleAsync(newUser, "USER");

                    await context.AddAsync(userClass);
                    await context.SaveChangesAsync();
                }
            }
        }


    }
}
