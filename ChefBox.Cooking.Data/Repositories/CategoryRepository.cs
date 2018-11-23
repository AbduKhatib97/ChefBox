using ChefBox.Cooking.Data.Repositories.Base;
using ChefBox.Cooking.Dto.Category;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.Model.Cooking;
using ChefBox.SqlServer.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ChefBox.Cooking.Data.Repositories
{
    public class CategoryRepository : BaseCookingRepository<Category,int>, ICategoryRepository
    {
        public CategoryRepository(ChefBoxDbContext context)
            : base(context)
        {
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var results = Context.Categories
                                 .Where(c => c.IsValid);

            var dtoResults = results.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
            }).AsEnumerable();

            return dtoResults;
        }

        public IEnumerable<RecipeDto> GetAllCategoryRecipes(int categoryId)
        {
            var results = Context.Recipes
                                 .Include(rec => rec.Category)
                                 .Where(rec => rec.IsValid && rec.CategoryId == categoryId);

            var dtoResults = results.Select(rec => new RecipeDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                RecipeType = rec.RecipeType,
            }).AsEnumerable();


            return dtoResults;
        }

        public CategoryDto GetCategory(int id)
        {
            var result = GetSingleOrDefaultBaseEntity(id, true);
            var dtoResult = new CategoryDto()
            {
                Id = result.Id,
                Name = result.Name,
            };
            return dtoResult;
        }

        public int GetRecipesCount(int categoryId)
        {
            return Context.Recipes
                          .Count(rec => rec.CategoryId == categoryId && rec.IsValid);
        }

        public bool RemoveCategory(int id)
        {
            return base.Remove(id);
        }

        public bool RemoveCategoryPermanently(int id)
        {
            return base.RemovePermanently(id);
        }

        public bool[] RemoveRangeCategories(params int[] ids)
        {
            return base.RemoveRange(ids);
        }

        public bool[] RemoveRangeCategoriesPermanently(params int[] ids)
        {
            return base.RemoveRangePermanently(ids);
        }
    }
}
