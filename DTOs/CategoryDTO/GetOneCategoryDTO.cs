
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CategoryDTO
{
	/// <summary>
	/// All Category Infos, With a List<Articles> nullable if not needed
	/// </summary>
	public class GetOneCategory
	{
		public int Id { get; set; }
		public string Titre { get; set; }
		public string Description { get; set; }
		public List<int>? ArticlesId { get; set; }

	}
}
