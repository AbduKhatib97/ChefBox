using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefBox.AdminUI.Controllers.Base;
using ChefBox.AdminUI.ViewModels.Ingredient;
using ChefBox.Cooking.Dto.Ingredient;
using ChefBox.Cooking.IData.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ChefBox.AdminUI.Extensions;
using System.Net;


namespace ChefBox.AdminUI.Controllers
{
    public class IngredientsController : BaseController
    {
        protected IIngredientRepository IngredientRepository { get; }
        public IngredientsController(ISharedRepository sharedRepository,IIngredientRepository ingredientRepository)
            :base(sharedRepository)
        {
            IngredientRepository = ingredientRepository;
        }

        [HttpGet]
        public IActionResult Index(string query = "")
        {
            var dtoResults = IngredientRepository.GetAllIngredientsWithDetails(query);

            var vm = new IngredientsTableViewModel()
            {
                IngredientDetails = dtoResults.Select(dto => new IngredientDetailViewModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    IngredientType = dto.IngredientType,
                    IngredientRecipesCount = dto.RecipesCount,
                }),
            };

            return View(@"~/Views/Ingredients/IngredientsTable.cshtml",vm);
        }
        [HttpGet]
        public IActionResult IngredientForm(int? id)
        {
            IngredientFormViewModel vm;

            if (id.HasValue && id > 0)
            {
                var dtoResult = IngredientRepository.GetIngredient(id.Value);

                vm = new IngredientFormViewModel()
                {
                        Id = dtoResult.Id,
                        Name = dtoResult.Name,
                        Description = dtoResult.Description,
                        IngredientType = dtoResult.IngredientType,
                };
            }
            else
            {
                vm = new IngredientFormViewModel();
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult IngredientForm(IngredientFormViewModel ingredientViewModel)
        {
            var data = IngredientRepository.ActionIngredient(new IngredientDto()
            {
                Id = ingredientViewModel.Id,
                Name = ingredientViewModel.Name,
                Description = ingredientViewModel.Description,
                IngredientType = ingredientViewModel.IngredientType,
            });

            if (data != null)
            {
                if (Request.IsAjaxRequest())
                {
                    return Json(data);
                }
                return RedirectToAction("Index");
            }

            return Content("Failed");
        }
        
        [HttpDelete]
        public IActionResult RemoveIngredient(int id)
        {
            var result = IngredientRepository.RemoveIngredient(id);

            if (result)
            {
                return Json(string.Empty);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

    }
}
