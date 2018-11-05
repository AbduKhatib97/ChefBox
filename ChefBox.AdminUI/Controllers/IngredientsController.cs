using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefBox.AdminUI.ViewModels.Ingredient;
using ChefBox.Cooking.Dto.Ingredient;
using ChefBox.Cooking.IData.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChefBox.AdminUI.Controllers
{
    public class IngredientsController : Controller
    {
        protected IIngredientRepository IngredientRepository { get; }
        public IngredientsController(IIngredientRepository ingredientRepository)
        {
            IngredientRepository = ingredientRepository;
        }
        public IActionResult Index(string query = "")
        {
            var dtoResults = IngredientRepository.GetAllIngredients(query);

            var vmResults = dtoResults.Select(dto => new IngredientDetailViewModel()
            {
                Ingredient = new IngredientViewModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    IngredientType = dto.IngredientType,
                },
                RecipesCount = IngredientRepository.GetRecipesCount(dto.Id),
            }).ToList();

            var vm = new IngredientsTableViewModel()
            {
                IngredientDetails = vmResults,
            };

            return View(@"~/Views/Ingredients/IngredientsTable.cshtml",vm);
        }
        public IActionResult IngredientForm(int id)
        {
            var ingredientViewModel = new IngredientFormViewModel();
            if (id == 0)
            {
                return View(@"~/Views/Ingredients/IngredientForm.cshtml");
            }
            else
            {
                var dtoResult = IngredientRepository.GetIngredient(id);

                ingredientViewModel.Ingredient = new IngredientViewModel()
                {
                    Id = dtoResult.Id,
                    Name = dtoResult.Name,
                    Description = dtoResult.Description,
                    IngredientType = dtoResult.IngredientType,
                };
                return View(@"~/Views/Ingredients/IngredientForm.cshtml", ingredientViewModel);
            }
        }
        [HttpPost]
        public IActionResult SaveForm(IngredientFormViewModel ingredientViewModel)
        {
            var data = IngredientRepository.ActionIngredient(new IngredientDto()
            {
                Id = ingredientViewModel.Ingredient.Id,
                Name = ingredientViewModel.Ingredient.Name,
                Description = ingredientViewModel.Ingredient.Description,
                IngredientType = ingredientViewModel.Ingredient.IngredientType,
            });
            if (data != null)
            {
                return RedirectToAction("Index");
            }
            return Content("Failed");
        }
    }
}
