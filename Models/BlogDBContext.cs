using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Models;

public class BlogDBContext : IdentityDbContext<AppUser>
{
    public BlogDBContext(DbContextOptions<BlogDBContext> options)
        : base(options)
    {
    }

    public DbSet<Article> Articles { get; set; }

    public DbSet<Categorie> Categories { get; set; }

    public DbSet<Commentaire> Commentaires { get; set; }

    public DbSet<DateTable> DateTables { get; set; }

    public DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
		{
			entity.HasKey(login => new { login.LoginProvider, login.ProviderKey });
		});

		// Additional entity configurations
		modelBuilder.Entity<Utilisateur>(entity =>
		{
			entity.HasKey(u => u.Id);
			entity.HasOne(u => u.AppUser)
				.WithOne(a => a.Utilisateur)
				.HasForeignKey<Utilisateur>(u => u.AppUserId);
		});
	}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=BlogDB2;Trusted_Connection=True;");
        }
        base.OnConfiguring(optionsBuilder);
    }
}