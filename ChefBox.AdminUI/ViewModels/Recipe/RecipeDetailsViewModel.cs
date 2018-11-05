using System.Collections.Generic;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipeDetailsViewModel
    {
        public RecipeViewModel Recipe { get; set; }
        public List<RecipeIngredientsViewModel> Ingredients { get; set; }
    }
}