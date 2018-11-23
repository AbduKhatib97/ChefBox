using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChefBox.AdminUI.Controllers.Base;
using ChefBox.AdminUI.ViewModels.Home;
using ChefBox.AdminUI.ViewModels.Home.Cards;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.ServiceExample.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChefBox.AdminUI.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ISharedRepository sharedRepository) : base(sharedRepository)
        {
        }

        public IActionResult Index()
        {
            var dtoResult = SharedRepository.GetStatisics();

            var vm = new IndexViewModel()
            {
                CategoryCard = new StatisticEntityCardViewModel()
                {
                    LatestItemId = dtoResult.CategoryStatistics.LatestItemId,
                    LatestItemName = dtoResult.CategoryStatistics.LatestItemName,
                    EntitiesCount = dtoResult.CategoryStatistics.EntitiesCount,
                },
                RecipeCard = new StatisticEntityCardViewModel()
                {
                    LatestItemId = dtoResult.RecipeStatistics.LatestItemId,
                    LatestItemName = dtoResult.RecipeStatistics.LatestItemName,
                    EntitiesCount = dtoResult.RecipeStatistics.EntitiesCount,
                },
                IngredientCard = new StatisticEntityCardViewModel()
                {
                    LatestItemId = dtoResult.IngredientStatistics.LatestItemId,
                    LatestItemName = dtoResult.IngredientStatistics.LatestItemName,
                    EntitiesCount = dtoResult.IngredientStatistics.EntitiesCount,
                },
                PhotoCard = new StatisticEntityCardViewModel()
                {
                    LatestItemId = dtoResult.PhotoStatistics.LatestItemId,
                    LatestItemName = dtoResult.PhotoStatistics.LatestItemName,
                    EntitiesCount = dtoResult.PhotoStatistics.EntitiesCount,
                },
            };

            return View(vm);
        }

    }
}
