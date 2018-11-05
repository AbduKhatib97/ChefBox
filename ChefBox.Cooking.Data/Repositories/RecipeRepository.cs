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
using System.Text;

namespace ChefBox.Cooking.Data.Repositories
{
    public class RecipeRepository : BaseRepository, IRecipeRepository
    {
        public RecipeRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public RecipeDto ActionRecipe(RecipeDto recipe)
        {
            try
            {
                var recipeEntity = GetSingleOrDefaultBaseEntity<Recipe>(recipe.Id, isValid: true);

                EntityState entityState = EntityState.Modified;
                if (recipeEntity is null)
                {
                    recipeEntity = new Recipe()
                    {
                        Name = recipe.Name,
                        Description = recipe.Description,
                        RecipeType = recipe.RecipeType,
                        CategoryId = recipe.CategoryId,
                        IsPublished = recipe.IsPublished,
                    };
                    entityState = EntityState.Added;
                } // Add case
                else
                {
                    recipeEntity.Name = recipe.Name;
                    recipeEntity.Description = recipe.Description;
                    recipeEntity.RecipeType = recipe.RecipeType;
                    recipeEntity.CategoryId = recipe.CategoryId;
                    recipeEntity.IsPublished = recipe.IsPublished;
                } // Update case
                Context.Entry(recipeEntity).State = entityState;
                Context.SaveChanges();
                recipe.Id = recipeEntity.Id;
                return recipe;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RecipeIngredientDto> GetAllRecipeIngredients(int recipeId)
        {
            var results = Context.RecipeIngredients
                                 .Include(ri => ri.Ingredient)
                                 .Where(ri => ri.RecipeId == recipeId && ri.IsValid)
                                 .Select(ri => new RecipeIngredientDto()
                                 {
                                     Id = ri.Id,
                                     IngredientId = ri.IngredientId,
                                     IngredientName = ri.Ingredient.Name,
                                     Amount = ri.Amount,
                                     Unit = ri.Unit,
                                     IsChecked =true,
                                 }).AsEnumerable();
            return results;
        }

        public IEnumerable<PhotoDto> GetAllRecipePhotos(int recipeId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RecipeDto> GetAllRecipes(bool? isPublished, string query = "")
        {
            var queryUpper = string.IsNullOrEmpty(query) ? "" : query.ToUpper();

            var results = Context.Recipes
                                 .Include(rec => rec.Category)
                                 .Where(rec =>
                                            rec.IsValid
                                            &&
                                            (isPublished.HasValue ? rec.IsPublished == isPublished : true)
                                            &&
                                            (
                                                string.IsNullOrEmpty(queryUpper)
                                                ||
                                                rec.Name.ToUpper().Contains(queryUpper)
                                                ||
                                                rec.Description.ToUpper().Contains(queryUpper)
                                                ||
                                                rec.RecipeType.ToString().ToUpper().Contains(queryUpper)
                                            )
                                       )
                                 .Select(rec => new RecipeDto()
                                 {
                                     Id = rec.Id,
                                     Name = rec.Name,
                                     Description = rec.Description,
                                     RecipeType = rec.RecipeType,
                                     IsPublished = rec.IsPublished,
                                     CategoryId = rec.CategoryId,
                                     CategoryName = rec.Category.Name,
                                 }).AsEnumerable();
            return results;
        }

        public string GetCategoryName(int recipeId)
        {
            var result = Context.Recipes
                                .Include(r => r.Category)
                                .SingleOrDefault(r => r.Id == recipeId && r.IsValid).Name;

            return result;
        }

        public RecipeDto GetRecipe(int id)
        {
            var result = GetSingleOrDefaultBaseEntity<Recipe>(id, true);
            var categoryName = GetCategoryName(result.Id);

            var dtoResult = new RecipeDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                RecipeType = result.RecipeType,
                IsPublished = result.IsPublished,
                CategoryId = result.CategoryId,
                CategoryName = categoryName,
            };

            return dtoResult;
        }

        public bool RemoveRangeRecipes(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRangeRecipesPermanently(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRecipe(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRecipePermanently(int id)
        {
            throw new NotImplementedException();
        }
    }
}
