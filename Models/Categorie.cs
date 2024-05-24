using System;
using System.Collections.Generic;

namespace Models;

public class Categorie
{
    public int Id { get; set; }

    public string Titre { get; set; }

    public string Description { get; set; }

    public List<Article> Articles { get; set; } = new List<Article>();
}