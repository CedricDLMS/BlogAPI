
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.CategoryDTO
{
	/// <summary>
	/// SimpleCategoryDTO
	/// </summary>
	public class GetOneCategorySimpleDTO
	{
		public int Id { get; set; }
		public string Titre { get; set; }
		public string Description { get; set; }

	}
}
