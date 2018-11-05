using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IRecipeRepository
    {
        IEnumerable<RecipeDto> GetAllRecipes(bool? isPublished, string query = "");
        string GetCategoryName(int recipeId);
        RecipeDto GetRecipe(int id);
        RecipeDto ActionRecipe(RecipeDto recipeDto);
        bool RemoveRecipe(int id);
        bool RemoveRangeRecipes(params int[] ids);
        bool RemoveRecipePermanently(int id);
        bool RemoveRangeRecipesPermanently(params int[] ids);
        IEnumerable<PhotoDto> GetAllRecipePhotos(int recipeId);
        IEnumerable<RecipeIngredientDto> GetAllRecipeIngredients(int recipeId);

    }
}
