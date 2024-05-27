using DTOs.CategoryDTO;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class CategoryService
	{
		private readonly CategoryRepository categoryRepository;
		public CategoryService(CategoryRepository category)
		{
			this.categoryRepository = category;
		}
		/// <summary>
		/// Made to create a category from a DTO 
		/// </summary>
		/// <returns>Return the category dto</returns>
		public async Task<GetOneCategory> CreateAsync(CreateCategoryDTO createCategoryDTO)
		{
			if(createCategoryDTO == null) { throw new Exception("Category can't be null"); }
			if(createCategoryDTO.Titre.IsNullOrEmpty()) { throw new Exception("Titre can't be null"); }
			if(createCategoryDTO.Titre.Any(ch => !char.IsLetterOrDigit(ch))) { throw new Exception("Titre can't contain special Character"); }
			if (createCategoryDTO.Titre.Length >= 15) { throw new Exception("Titre can't exceed 15 chars"); }
			if (createCategoryDTO.Titre.Length <= 3) { throw new Exception("Titre can't be less 3 chars"); }

			if (createCategoryDTO.Description.Length >= 100) { throw new Exception("Description can't exceed 100 chars"); }

			// Si tout les tests du dessus effectué , lance la creation !
			try
			{
				return await categoryRepository.CreateCategory(createCategoryDTO);
			}catch(Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}
		/// <summary>
		/// Get A list of all categories
		/// </summary>
		/// <returns>Return a List, empty if no categories</returns>
		/// <exception cref="Exception"></exception>
		public async Task<List<CategoryListDTO>?> GetAll()
		{
			try
			{
				return await this.categoryRepository.GetAllCategory();
			}catch(Exception ex) { throw new Exception(ex.Message, ex); }
		}


	}
}
