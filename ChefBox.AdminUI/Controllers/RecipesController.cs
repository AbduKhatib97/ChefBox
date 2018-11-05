using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.AdminUI.ViewModels.Recipe;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChefBox.Cooking.Dto.Recipe;

namespace ChefBox.AdminUI.Controllers
{
    public class RecipesController : Controller
    {
        public IRecipeRepository RecipeRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public RecipesController(IRecipeRepository recipeRepository,ICategoryRepository categoryRepository)
        {
            RecipeRepository = recipeRepository;
            CategoryRepository = categoryRepository;
        }

        public IActionResult Index(string query = "")
        {
            var dtoResults = RecipeRepository.GetAllRecipes(null ,query);

            var vmResults = dtoResults.Select(dto => new RecipeDetailsViewModel()
            {
                Recipe = new RecipeViewModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    RecipeType = dto.RecipeType,
                    IsPublished = dto.IsPublished,
                    CategoryId = dto.CategoryId,
                    CategoryName = dto.CategoryName,
                },
                Ingredients = RecipeRepository.GetAllRecipeIngredients(dto.Id)
                                              .Select(riDto => new RecipeIngredientsViewModel()
                                              {
                                                  Id = riDto.Id,
                                                  IngredientId = riDto.IngredientId,
                                                  IngredientName = riDto.IngredientName,
                                                  Amount = riDto.Amount,
                                                  Unit = riDto.Unit,
                                              }).ToList(),
            }).ToList();

            var vm = new RecipesTableViewModel()
            {
                Recipes = vmResults,
            };

            return View(@"~/Views/Recipes/RecipesTable.cshtml",vm);
        }
        //public IActionResult RecipeForm()
        //{
        //    var vm = new RecipeFormViewModel()
        //    {
        //        Categories = CategoryRepository.GetAllCategories()
        //                                       .Select(c => new SelectListItem()
        //                                       {
        //                                           Value = c.Id.ToString(),
        //                                           Text = c.Name,
        //                                       }),
        //    };

        //    return View(@"~/Views/Recipes/RecipeForm.cshtml", vm);
        //}
        public IActionResult RecipeForm(int id)
        {
            var vm = new RecipeFormViewModel();

            if (id == 0)
            {
                vm.Recipe = null;
                vm.Categories = CategoryRepository.GetAllCategories()
                                                  .Select(c => new SelectListItem()
                                                  {
                                                      Value = c.Id.ToString(),
                                                      Text = c.Name,
                                                  }).ToList();
            }
            else
            {
                var dtoResult = RecipeRepository.GetRecipe(id);

                vm.Recipe = new RecipeViewModel()
                {
                    Id = dtoResult.Id,
                    Name = dtoResult.Name,
                    Description = dtoResult.Description,
                    RecipeType = dtoResult.RecipeType,
                    IsPublished = dtoResult.IsPublished,
                    CategoryId = dtoResult.CategoryId,
                };

                vm.Categories = CategoryRepository.GetAllCategories()
                                                  .Select(c => new SelectListItem()
                                                  {
                                                      Value = c.Id.ToString(),
                                                      Text = c.Name,
                                                  }).ToList();
            }
            return View(@"~/Views/Recipes/RecipeForm.cshtml", vm);
        }
        [HttpPost]
        public IActionResult SaveForm(RecipeFormViewModel recipeForm)
        {
            var recipeViewModel = recipeForm.Recipe;

            var data = new RecipeDto()
            {
                Id = recipeViewModel.Id,
                Name = recipeViewModel.Name,
                Description = recipeViewModel.Description,
                RecipeType = recipeViewModel.RecipeType,
                IsPublished = recipeViewModel.IsPublished,
                CategoryId = recipeViewModel.CategoryId,
            };

            RecipeRepository.ActionRecipe(data);
            return RedirectToAction("Index");
        }
    }
}
