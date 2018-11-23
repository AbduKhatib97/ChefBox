using System.Collections.Generic;
using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.AdminUI.ViewModels.Photo;

namespace ChefBox.AdminUI.ViewModels.Recipe {
    public class RecipeDetailsViewModel : RecipeViewModel {

        public IEnumerable<PhotoViewModel> Photos { get; set; }
        public IEnumerable<RecipeIngredientsViewModel> Ingredients { get; set; }
    }
}