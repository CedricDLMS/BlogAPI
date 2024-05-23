using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Models;

public class AppUser : IdentityUser
{
    public Utilisateur Utilisateur { get; set; }
}