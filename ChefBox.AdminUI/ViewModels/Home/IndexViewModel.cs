using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.AdminUI.ViewModels.Home.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.ViewModels.Home
{
    public class IndexViewModel :SharedViewModel
    {
        public StatisticEntityCardViewModel IngredientCard { get; set; }
        public StatisticEntityCardViewModel RecipeCard { get; set; }
        public StatisticEntityCardViewModel CategoryCard { get; set; }
        public StatisticEntityCardViewModel PhotoCard { get; set; }

    }
}
