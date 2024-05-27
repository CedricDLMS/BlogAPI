using DTOs.CategoryDTO;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
	public class CategoryRepository
	{
		private readonly BlogDBContext _context;
		public CategoryRepository(BlogDBContext context)
		{
			_context = context;
		}


		public async Task<GetOneCategory> CreateCategory(CreateCategoryDTO createCategoryDTO)
		{
			Categorie newCategory = new Categorie
			{
				Titre = createCategoryDTO.Titre,
				Description = createCategoryDTO.Description,
			};

			await this._context.AddAsync(newCategory);
			var result = this._context.SaveChanges(); 
			if(result == 0)
			{
				throw new Exception("Something went wrong while saving"); // Si pas de ligne modifié en DB , erreur de sauvegarde
			}

			return new GetOneCategory { Id = newCategory.Id, Description = newCategory.Description, Titre = newCategory.Titre };
		}

		public async Task<List<CategoryListDTO>?> GetAllCategory()
		{
			return await this._context.Categories.Select(model => new CategoryListDTO
			{
				Id = model.Id,
				Titre = model.Titre,
				Description = model.Description
			}).ToListAsync();
		}

		public async Task<GetOneCategory?> GetOneCategoryDetails(int Id)
		{
			return await this._context.Categories
				.Include(o => o.Articles)
				.Select(model => new GetOneCategory
				{
					Id = model.Id,
					Description = model.Description,
					Titre = model.Titre,
					ArticlesId = model.Articles.Select(o=>o.Id).ToList()
				}).FirstOrDefaultAsync( Categorie => Categorie.Id == Id);
		}
		public async Task<GetOneCategorySimpleDTO?> GetOneCategorySimple(int Id)
		{
			return await this._context.Categories
				.Include(o => o.Articles)
				.Select(model => new GetOneCategorySimpleDTO
				{
					Id = model.Id,
					Description = model.Description,
					Titre = model.Titre,
				}).FirstOrDefaultAsync(Categorie => Categorie.Id == Id);
		}

		public async Task<int> UpdateCategoryAsync(GetOneCategorySimpleDTO dTO)
		{

			var categoryToUpdate = await _context.Categories.FirstOrDefaultAsync(c => c.Id == dTO.Id);

			if (categoryToUpdate != null)
			{
				categoryToUpdate.Description = dTO.Description;
				categoryToUpdate.Titre = dTO.Titre;

				_context.Categories.Update(categoryToUpdate);
				return await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("No entities found");
			}

			#region comment
			//var categoryToUpdate = await this.GetOneCategorySimple(dTO.Id);
			//if (categoryToUpdate != null)
			//{
			//	categoryToUpdate.Description = dTO.Description;
			//	categoryToUpdate.Titre = dTO.Titre;
			//	this._context.Update(new Categorie { Titre = dTO.Titre , Description = dTO.Description, Id = dTO.Id});
			//	return await this._context.SaveChangesAsync();
			//}
			//else
			//{
			//	throw new Exception("No entities found");
			//}
			#endregion
		}

		public async Task<int> DeleteCategoryAsync(int id)
		{
			var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

			if (categoryToDelete != null)
			{
				_context.Categories.Remove(categoryToDelete);
				return await _context.SaveChangesAsync();
			}
			else
			{
				throw new Exception("No entities found with the given ID");
			}
		}
	}
}
