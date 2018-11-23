using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChefBox.AdminUI.Controllers.Base;
using ChefBox.AdminUI.Extensions;
using ChefBox.AdminUI.ViewModels.Photo;
using ChefBox.AdminUI.ViewModels.Recipe;
using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.Enum.Cooking.Enums;
using ChefBox.ServiceExample.Controllers;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace ChefBox.AdminUI.Controllers
{
    public class RecipesController : BaseController
    {
        private const string RecipeImagesFolder = "recipeImages";

        public IRecipeRepository RecipeRepository { get; }
        public IHostingEnvironment HostingEnvironment { get; }
        public ICategoryRepository CategoryRepository { get; }
        public RecipesController(ISharedRepository sharedRepository, IRecipeRepository recipeRepository, ICategoryRepository categoryRepository,
            IHostingEnvironment hostingEnvironment) : base(sharedRepository)
        {
            RecipeRepository = recipeRepository;
            CategoryRepository = categoryRepository;
            HostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(string query = null, int? categoryId = null, RecipeType? recipeType = null, bool? IsPublished = null)
        {

            var dtoResults = RecipeRepository.GetAllRecipesWithCoverPhoto(new RecipesFilterCriteria()
            {
                Query = query,
                CategoryId = categoryId,
                RecipeType = recipeType,
                IsPublished = IsPublished,
            });

            //var htc = new HttpClient();
            //var r = await htc.GetAsync(@"http://localhost:11893/api/values");
            //var c = await r.Content.ReadAsStringAsync();

            //var dtoResults = JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(c);

            var vm = new IndexViewModel()
            {
                RecipesCards = dtoResults.Select(dto => new RecipeCardViewModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description.FromHtmlToInnerText(),
                    RecipeType = dto.RecipeType,
                    IsPublished = dto.IsPublished,
                    CategoryId = dto.CategoryId,
                    CategoryName = dto.CategoryName,
                    CreationDate = dto.CreationDate,

                    Photo = dto.CoverPhoto is null? null : new PhotoViewModel()
                    {
                        Id=dto.CoverPhoto.Id,
                        Name = dto.CoverPhoto.Name,
                        Url = dto.CoverPhoto.Url,
                    }
                }).ToList(),
            };

            return View(vm);

            //return Json(JsonConvert.DeserializeObject<IEnumerable<RecipeDto>>(c)); 

        }

        public IActionResult RecipeDetails(int id)
        {
            var dtoResult = RecipeRepository.GetRecipeDetails(id);

            var vm = new RecipeDetailsViewModel()
            {
                Id = dtoResult.Id,
                Name = dtoResult.Name,
                Description = dtoResult.Description,
                RecipeType = dtoResult.RecipeType,
                CategoryId = dtoResult.CategoryId,
                CategoryName = dtoResult.CategoryName,
                IsPublished = dtoResult.IsPublished,
                Ingredients = dtoResult.RecipeIngredients.Select(ri => new RecipeIngredientsViewModel()
                {
                    Id = ri.Id,
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.IngredientName,
                    Amount = ri.Amount,
                    Unit = ri.Unit,
                }),
                Photos = dtoResult.Photos.Select(ph => new PhotoViewModel()
                {
                    Id = ph.Id,
                    Name = ph.Name,
                    Url = ph.Url,
                }),
            };

            return View(vm);
        }
        public IActionResult RecipesTable(string query, int? categoryId, RecipeType? recipeType, bool? IsPublished)
        {
            var filter = new RecipesFilterCriteria()
            {
                Query = query,
                CategoryId = categoryId,
                RecipeType = recipeType,
                IsPublished = IsPublished,
            };

            var dtoResults = RecipeRepository.GetAllRecipesWithIngredientsDetails(filter);

            var vm = new RecipesTableViewModel()
            {
                Recipes = dtoResults.Select(dto => new RecipeDetailsViewModel()
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Description = dto.Description,
                    RecipeType = dto.RecipeType,
                    IsPublished = dto.IsPublished,
                    CategoryId = dto.CategoryId,
                    CategoryName = dto.CategoryName,
                    Ingredients = dto.Ingredients.Select(riDto => new RecipeIngredientsViewModel()
                    {
                        Id = riDto.Id,
                        IngredientId = riDto.IngredientId,
                        IngredientName = riDto.IngredientName,
                        Amount = riDto.Amount,
                        Unit = riDto.Unit,
                    }),
                }),
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult RecipeForm(int? id)
        {
            RecipeFormViewModel vm;
            var categories = CategoryRepository.GetAllCategories().Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Name,
            });

            if (id.HasValue && id > 0)
            {
                var dtoResult = RecipeRepository.GetRecipe(id.Value);

                vm = new RecipeFormViewModel()
                {
                    Id = dtoResult.Id,
                    Name = dtoResult.Name,
                    Description = dtoResult.Description,
                    RecipeType = dtoResult.RecipeType,
                    IsPublished = dtoResult.IsPublished,
                    CategoryId = dtoResult.CategoryId,
                    Categories = categories,
                };
            }
            else
            {
                vm = new RecipeFormViewModel()
                {
                    Categories = categories,
                };
            }

            return View(vm);
        }

        [HttpPost]
        public IActionResult RecipeForm(RecipeFormViewModel recipeForm)
        {

            try
            {
                List<PhotoViewModel> photos = SavePhotosOnHard(recipeForm);

                var result = RecipeRepository.ActionRecipe(new RecipeFormDto()
                {
                    Id = recipeForm.Id,
                    Name = recipeForm.Name,
                    CategoryId = recipeForm.CategoryId,
                    RecipeType = recipeForm.RecipeType,
                    Description = recipeForm.Description,
                    Photos = photos.Select(vm => new PhotoDto()
                    {
                        Name = vm.Name,
                        Url = vm.Url,
                    }),
                    IsPublished = recipeForm.IsPublished,
                    RecipeIngredients = null,

                });
                if (result is null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("RecipeDetails", "Recipes", new { id = result.Id });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private List<PhotoViewModel> SavePhotosOnHard(RecipeFormViewModel recipeForm)
        {
            var wwwrootPath = HostingEnvironment.WebRootPath;
            var recipeImagesFolderPath = Path.Combine(wwwrootPath, RecipeImagesFolder);
            string photoNameOnHard = string.Empty;
            string fullPhotoPath = string.Empty;
            List<PhotoViewModel> photos = new List<PhotoViewModel>();
            if (recipeForm.Photos != null)
            {
                foreach (var photo in recipeForm.Photos)
                {
                    photoNameOnHard = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                    fullPhotoPath = Path.Combine(recipeImagesFolderPath, photoNameOnHard);
                    using (var fileStream = new FileStream(fullPhotoPath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);
                        photos.Add(new PhotoViewModel()
                        {
                            Name = photo.Name,
                            Url = $"/{RecipeImagesFolder}/{photoNameOnHard}"
                        });
                    }
                }
            }

            return photos;
        }

        [HttpDelete]
        public IActionResult RemoveRecipe(int id)
        {
            var result = RecipeRepository.RemoveRecipe(id);

            if (result)
            {
                return Json(string.Empty);
            }

            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

    }
}