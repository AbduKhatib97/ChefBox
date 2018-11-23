using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Enum.Cooking.Enums;
using System.Collections.Generic;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IRecipeRepository
    {
        IEnumerable<RecipeDto> GetAllRecipes(RecipesFilterCriteria filterCriteria);
        IEnumerable<RecipeWithIngredientsDetailsDto> GetAllRecipesWithIngredientsDetails(RecipesFilterCriteria filterCriteria);
        IEnumerable<RecipeWithCoverPhotoDto> GetAllRecipesWithCoverPhoto(RecipesFilterCriteria filterCriteria);
        IEnumerable<RecipeWithFullDetailsDto> GetAllRecipesWithFullDetails(RecipesFilterCriteria filterCriteria);
        IEnumerable<RecipeDto> GetAllRecipes(params int[] ids);
        RecipeDto GetRecipe(int id);
        RecipeDto ActionRecipe(RecipeFormDto recipeFormDto);
        bool RemoveRecipe(int id);
        bool[] RemoveRangeRecipes(params int[] ids);
        bool RemoveRecipePermanently(int id);
        bool[] RemoveRangeRecipesPermanently(params int[] ids);
        IEnumerable<RecipeIngredientDto> GetAllRecipeIngredients(int recipeId);
        RecipeWithFullDetailsDto GetRecipeDetails(int id);
    }
}
