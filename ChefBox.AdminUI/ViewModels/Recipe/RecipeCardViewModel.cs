
using ChefBox.AdminUI.ViewModels.Photo;
using System;
using System.Collections.Generic;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipeCardViewModel : RecipeViewModel
    {
        public PhotoViewModel Photo { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
