using ChefBox.Cooking.Dto.Photo;
using ChefBox.Enum.Cooking.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeFormDto : RecipeDto
    {
        public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
    }

}