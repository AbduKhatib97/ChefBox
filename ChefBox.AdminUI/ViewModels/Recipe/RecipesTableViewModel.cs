using ChefBox.AdminUI.ViewModels.Base;
using System.Collections.Generic;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipesTableViewModel : SharedViewModel
    {
        public IEnumerable<RecipeDetailsViewModel> Recipes { get; set; }
    }
}
