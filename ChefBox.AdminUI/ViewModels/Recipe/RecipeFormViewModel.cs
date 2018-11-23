using System.Collections.Generic;
using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.AdminUI.ViewModels.Photo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChefBox.AdminUI.ViewModels.Recipe {
    public class RecipeFormViewModel : RecipeViewModel {
        public IEnumerable<IFormFile> Photos { get; set; }
        public IEnumerable<RecipeIngredientsViewModel> RecipeIngredients { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}