using System.ComponentModel.DataAnnotations;

namespace ChefBox.AdminUI.ViewModels.Ingredient
{
    public class IngredientDetailViewModel
    {
        public IngredientViewModel Ingredient { get; set; }
        [Display(Name = "Recipes Count")]
        public int RecipesCount { get; set; }

    }
}
