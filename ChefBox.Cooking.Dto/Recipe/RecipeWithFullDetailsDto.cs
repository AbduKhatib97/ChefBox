using ChefBox.Cooking.Dto.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeWithFullDetailsDto : RecipeDto
    {
        public IEnumerable<PhotoDto> Photos { get; set; }
        public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; }
    }
}
