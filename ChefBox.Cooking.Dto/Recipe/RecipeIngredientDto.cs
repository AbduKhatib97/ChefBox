using ChefBox.Enum.Cooking.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeIngredientDto
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        [Required]
        public string IngredientName { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        public bool IsChecked { get; set; }
    }
}
