using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.Dto.Ingredient
{
    public class IngredientDetailsDto
    {
        public IngredientDto Ingredient { get; set; }
        public int RecipesCount { get; set; }
    }
}
