using ChefBox.Cooking.Data.Repositories.Base;
using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.Model.Cooking;
using ChefBox.SqlServer.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChefBox.Cooking.Data.Repositories
{
    public class RecipeRepository : BaseCookingRepository<Recipe, int>, IRecipeRepository
    {
        public RecipeRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public RecipeDto ActionRecipe(RecipeFormDto recipeForm)
        {
            try
            {
                var recipeEntity = GetSingleOrDefaultBaseEntity(recipeForm.Id, isValid: true);

                EntityState entityState = EntityState.Modified;
                if (recipeEntity is null)
                {
                    recipeEntity = new Recipe()
                    {
                        Name = recipeForm.Name,
                        Description = recipeForm.Description,
                        RecipeType = recipeForm.RecipeType,
                        CategoryId = recipeForm.CategoryId,
                        IsPublished = recipeForm.IsPublished,
                        ModificationDate = DateTime.UtcNow,
                        Photos = recipeForm.Photos?.Select(ph => new Photo()
                        {
                            Name = ph.Name,
                            Url = ph.Url,
                            ModificationDate = DateTime.UtcNow,
                            IsCover = ph.IsCover,
                        }).ToList(),
                        RecipeIngredients = recipeForm.RecipeIngredients?.Where(ri => ri.IsChecked)
                        .Select(ri => new RecipeIngredient()
                        {
                            IngredientId = ri.IngredientId,
                            Amount = ri.Amount,
                            Unit = ri.Unit,
                            ModificationDate = DateTime.UtcNow,
                        }).ToList(),

                    };
                    entityState = EntityState.Added;
                } // Add case
                else
                {
                    recipeEntity.Name = recipeForm.Name;
                    recipeEntity.Description = recipeForm.Description;
                    recipeEntity.RecipeType = recipeForm.RecipeType;
                    recipeEntity.CategoryId = recipeForm.CategoryId;
                    recipeEntity.IsPublished = recipeForm.IsPublished;
                    recipeEntity.ModificationDate = DateTime.UtcNow;

                    RemoveRecipeIngredients(recipeEntity.Id);

                    recipeEntity.RecipeIngredients = recipeForm.RecipeIngredients?
                    .Where(riDto => riDto.IsChecked)
                    .Select(riDto => new RecipeIngredient()
                    {
                        IngredientId = riDto.IngredientId,
                        Amount = riDto.Amount,
                        Unit = riDto.Unit
                    }).ToList();

                    foreach (var photoDto in recipeForm.Photos)
                    {
                        recipeEntity.Photos.Add(new Photo()
                        {
                            Name = photoDto.Name,
                            Url = photoDto.Url
                        });

                    }
                } // Update case
                    Context.Entry(recipeEntity).State = entityState;
                    Context.SaveChanges();
                    recipeForm.Id = recipeEntity.Id;
                    return recipeForm;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private bool RemoveRecipeIngredients(int recipeId)
        {
            try
            {
                var recipeIngredients = Context.RecipeIngredients
                    .Where(ri => ri.RecipeId == recipeId)
                    .ToList();
                Context.RecipeIngredients.RemoveRange(recipeIngredients);
                Context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<RecipeIngredientDto> GetAllRecipeIngredients(int recipeId)
        {
            var results = Context.RecipeIngredients
                                 .Include(ri => ri.Ingredient)
                                 .Where(ri => ri.RecipeId == recipeId && ri.IsValid);

            var dtoResults = results.Select(ri => 
            new RecipeIngredientDto()
            {
                Id = ri.Id,
                IngredientId = ri.IngredientId,
                IngredientName = ri.Ingredient.Name,
                Amount = ri.Amount,
                Unit = ri.Unit,
                IsChecked =true,
            }).AsEnumerable();
            return dtoResults;
        }

        public IEnumerable<RecipeDto> GetAllRecipes(RecipesFilterCriteria filterCriteria)
        {
            var results = GetRecipesQuery(filterCriteria);

            var dtoResults = results.OrderByDescending(dto => dto.ModificationDate).Select(rec => new RecipeDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                RecipeType = rec.RecipeType,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                CreationDate = rec.CreationDate,
            }).AsEnumerable();

            return dtoResults;
        }


        public IEnumerable<RecipeDto> GetAllRecipes(params int[] ids)
        {
            var results = GetRecipesQuery(ids);

            var dtoResults = results.OrderByDescending(dto => dto.ModificationDate).Select(rec => new RecipeDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                RecipeType = rec.RecipeType,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                CreationDate = rec.CreationDate,

            }).AsEnumerable();

            return dtoResults;
        }

        public IEnumerable<RecipeWithCoverPhotoDto> GetAllRecipesWithCoverPhoto(RecipesFilterCriteria filterCriteria)
        {

            var results = GetRecipesQuery(filterCriteria);

            var dtoResults = results.OrderByDescending(dto => dto.ModificationDate).Select(rec => new RecipeWithCoverPhotoDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                RecipeType = rec.RecipeType,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                CreationDate = rec.CreationDate,
                CoverPhoto = rec.Photos.Where(ph => ph.IsValid && ph.IsCover).Select(ph => new PhotoDto()
                {
                    Id = ph.Id,
                    Name = ph.Name,
                    Url = ph.Url,
                    IsCover = ph.IsCover,
                }).FirstOrDefault(),
            }).AsEnumerable();


            return dtoResults;
        }

        public IEnumerable<RecipeWithFullDetailsDto> GetAllRecipesWithFullDetails(RecipesFilterCriteria filterCriteria)
        {
            var results = GetRecipesQuery(filterCriteria);

            var dtoResults = results.OrderByDescending(dto => dto.ModificationDate).Select(rec => new RecipeWithFullDetailsDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                RecipeType = rec.RecipeType,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                CreationDate = rec.CreationDate,
                RecipeIngredients = rec.RecipeIngredients.Where(ri => ri.IsValid).Select(ri => new RecipeIngredientDto()
                {
                    Id = ri.Id,
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    Unit = ri.Unit,
                    IsChecked = true,
                }),
                Photos = rec.Photos.Where(ph => ph.IsValid).Select(ph => new PhotoDto()
                {
                    Id = ph.Id,
                    Name = ph.Name,
                    Url = ph.Url,
                    IsCover = ph.IsCover,
                }),
            }).AsEnumerable();

            return dtoResults;
        }

        public IEnumerable<RecipeWithIngredientsDetailsDto> GetAllRecipesWithIngredientsDetails(RecipesFilterCriteria filterCriteria)
        {
            var results = GetRecipesQuery(filterCriteria);

            var dtoResults = results.OrderByDescending(dto => dto.ModificationDate).Select(rec =>
            new RecipeWithIngredientsDetailsDto()
            {
                Id = rec.Id,
                Name = rec.Name,
                Description = rec.Description,
                RecipeType = rec.RecipeType,
                IsPublished = rec.IsPublished,
                CategoryId = rec.CategoryId,
                CategoryName = rec.Category.Name,
                CreationDate = rec.CreationDate,
                Ingredients = rec.RecipeIngredients.Where(ri => ri.IsValid).Select(ri => new RecipeIngredientDto()
                {
                    Id = ri.Id,
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    Unit = ri.Unit,
                    IsChecked =true,
                })
            }).AsEnumerable();

            return dtoResults;
        }


        public RecipeDto GetRecipe(int id)
        {
            var result = Context.Recipes
                                .Include(r => r.Category)
                                .SingleOrDefault(r => r.Id == id && r.IsValid);

            var dtoResult = new RecipeDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                RecipeType = result.RecipeType,
                IsPublished = result.IsPublished,
                CategoryId = result.CategoryId,
                CategoryName = result.Category.Name,
            };

            return dtoResult;
        }

        public bool[] RemoveRangeRecipes(params int[] ids)
        {
            return base.RemoveRange(ids);
        }

        public bool[] RemoveRangeRecipesPermanently(params int[] ids)
        {
            return base.RemoveRangePermanently(ids);
        }

        public bool RemoveRecipe(int id)
        {
            return base.Remove(id);
        }

        public bool RemoveRecipePermanently(int id)
        {
            return base.RemovePermanently(id);
        }

        #region IQueryableResults

        private IQueryable<Recipe> GetRecipesQuery(int[] ids)
        {
            return Context.Recipes
                                 .Include(rec => rec.Category)
                                 .Include(rec => rec.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                 .Include(rec => rec.Photos)
                                 .Where(rec => rec.IsValid && ids.Contains(rec.Id));
        }

        private IQueryable<Recipe> GetRecipesQuery(RecipesFilterCriteria filterCriteria)
        {
            var queryUpper = string.IsNullOrEmpty(filterCriteria.Query) ? "" : filterCriteria.Query.ToUpper();

            var results = Context.Recipes
                                 .Include(rec => rec.RecipeIngredients).ThenInclude(ri => ri.Ingredient)
                                 .Include(rec => rec.Category)
                                 .Include(rec => rec.Photos)
                                 .Where(rec =>
                                            rec.IsValid
                                            &&
                                            filterCriteria.IsPublished.HasValue ? rec.IsPublished.Equals(filterCriteria.IsPublished) : true
                                            &&
                                            (
                                                string.IsNullOrEmpty(queryUpper)
                                                ||
                                                rec.Name.ToUpper().Contains(queryUpper)
                                                ||
                                                rec.Description.ToUpper().Contains(queryUpper)
                                            )
                                            &&
                                            filterCriteria.CategoryId.HasValue ? rec.CategoryId.Equals(filterCriteria.CategoryId) : true
                                            &&
                                            filterCriteria.RecipeType.HasValue ? rec.RecipeType.Equals(filterCriteria.RecipeType) : true
                                       );
            return results;
        }

        public RecipeWithFullDetailsDto GetRecipeDetails(int id)
        {
            var result = GetRecipesQuery(new[] { id }).SingleOrDefault();

            var dtoResult = new RecipeWithFullDetailsDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                RecipeType = result.RecipeType,
                IsPublished = result.IsPublished,
                CategoryId = result.CategoryId,
                CategoryName = result.Category.Name,
                RecipeIngredients = result.RecipeIngredients.Where(ri => ri.IsValid).Select(ri => new RecipeIngredientDto()
                {
                    Id = ri.Id,
                    IngredientId = ri.IngredientId,
                    IngredientName = ri.Ingredient.Name,
                    Amount = ri.Amount,
                    Unit = ri.Unit,
                    IsChecked = true,
                }),
                Photos = result.Photos.Where(ph => ph.IsValid).Select(ph => new PhotoDto()
                {
                    Id = ph.Id,
                    Name = ph.Name,
                    Url = ph.Url,
                    IsCover = ph.IsCover,
                }),

            };

            return dtoResult;
        }


        #endregion
    }
}
