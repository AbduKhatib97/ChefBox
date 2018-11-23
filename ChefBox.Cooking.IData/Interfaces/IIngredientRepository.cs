using ChefBox.Cooking.Dto.Ingredient;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Model.Cooking;
using System.Collections.Generic;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientDto> GetAllIngredients(string query = "");
        IEnumerable<IngredientDetailsDto> GetAllIngredientsWithDetails(string query = "");
        IngredientDto GetIngredient(int id);
        IngredientDto ActionIngredient(IngredientDto ingredientDto);
        bool RemoveIngredient(int id);
        bool[] RemoveRangeIngredients(params int[] ids);
        bool RemoveIngredientPermanently(int id);
        bool[] RemoveRangeIngredientsPermanently(params int[] ids);
        IEnumerable<RecipeDto> GetAllIngredientRecipes(int ingredientId);

    }
}
