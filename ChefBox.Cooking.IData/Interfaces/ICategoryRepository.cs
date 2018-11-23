using ChefBox.Cooking.Dto.Category;
using ChefBox.Cooking.Dto.Recipe;
using System.Collections.Generic;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface ICategoryRepository
    {
        CategoryDto GetCategory(int id);
        IEnumerable<CategoryDto> GetAllCategories();
        IEnumerable<RecipeDto> GetAllCategoryRecipes(int categoryId);
        int GetRecipesCount(int categoryId);
        bool RemoveCategory(int id);
        bool[] RemoveRangeCategories(params int[] ids);
        bool RemoveCategoryPermanently(int id);
        bool[] RemoveRangeCategoriesPermanently(params int[] ids);

    }
}
