using ChefBox.AdminUI.ViewModels.Base;
using System.Collections.Generic;

namespace ChefBox.AdminUI.ViewModels.Ingredient
{
    public class IngredientsTableViewModel : SharedViewModel
    {
        public IngredientFormViewModel IngredientForm { get; set; }
        public IEnumerable<IngredientDetailViewModel> IngredientDetails { get; set; }
    }
}
