using ChefBox.AdminUI.ViewModels.Category;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipeFormViewModel
    {
        public RecipeViewModel Recipe { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
