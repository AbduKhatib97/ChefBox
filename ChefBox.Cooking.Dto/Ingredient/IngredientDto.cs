using ChefBox.Enum.Cooking.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.Cooking.Dto.Ingredient
{
    public class IngredientDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public IngredientType IngredientType { get; set; }
    }
}
