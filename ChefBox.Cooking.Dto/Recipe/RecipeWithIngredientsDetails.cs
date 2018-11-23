using System.Collections.Generic;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeWithIngredientsDetailsDto : RecipeDto
    {
        public IEnumerable<RecipeIngredientDto> Ingredients { get; set; }
    }
}