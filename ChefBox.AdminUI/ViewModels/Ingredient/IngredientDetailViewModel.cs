using System.ComponentModel.DataAnnotations;
using ChefBox.AdminUI.ViewModels.Base;

namespace ChefBox.AdminUI.ViewModels.Ingredient {
    public class IngredientDetailViewModel : IngredientViewModel {

        [Display (Name = "Recipes Count")]
        public int IngredientRecipesCount { get; set; }

    }
}