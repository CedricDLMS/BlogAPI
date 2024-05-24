using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Models;

public class AppUser : IdentityUser
{
    //public string UtilisateurId { get; set; }
    public Utilisateur Utilisateur { get; set; }
}