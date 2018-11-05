using ChefBox.Cooking.Dto.Ingredient;
using ChefBox.Cooking.Dto.Recipe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<IngredientDto> GetAllIngredients(string query = null);
        IngredientDto GetIngredient(int id);
        int GetRecipesCount(int ingredientId);
        IngredientDto ActionIngredient(IngredientDto ingredientDto);
        bool RemoveIngredient(int id);
        bool RemoveRangeIngredients(params int[] ids);
        bool RemoveIngredientPermanently(int id);
        bool RemoveRangeIngredientsPermanently(params int[] ids);
        IEnumerable<RecipeFormDto> GetAllIngredientRecipes(int ingredientId);

    }
}
