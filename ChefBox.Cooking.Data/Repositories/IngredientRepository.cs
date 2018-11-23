using ChefBox.Cooking.Data.Repositories.Base;
using ChefBox.Cooking.Dto.Ingredient;
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
    public class IngredientRepository : BaseCookingRepository<Ingredient,int>, IIngredientRepository
    {
        public IngredientRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public IngredientDto ActionIngredient(IngredientDto ingredientDto)
        {
            try
            {
                var ingredientEntity = GetSingleOrDefaultBaseEntity(ingredientDto.Id,true);
                EntityState entityState = EntityState.Modified;
                if (ingredientEntity is null)
                {
                    ingredientEntity = new Ingredient()
                    {
                        Name = ingredientDto.Name,
                        Description = ingredientDto.Description,
                        IngredientType = ingredientDto.IngredientType,
                    };
                    entityState = EntityState.Added;
                }//Add case
                else
                {
                    ingredientEntity.Name = ingredientDto.Name;
                    ingredientEntity.Description = ingredientDto.Description;
                    ingredientEntity.IngredientType = ingredientDto.IngredientType;
                }//Update case
                Context.Entry(ingredientEntity).State = entityState;
                Context.SaveChanges();
                ingredientDto.Id = ingredientEntity.Id;
                return ingredientDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RecipeDto> GetAllIngredientRecipes(int ingredientId)
        {
            var idResults = Context.RecipeIngredients.Include(ri => ri.Ingredient)
                                   .Where(ri => ri.IngredientId == ingredientId && ri.IsValid)
                                   .Select(ri => ri.Id)
                                   .ToArray();

            var dtoResults = new RecipeRepository(Context).GetAllRecipes(idResults);

            return dtoResults;
        }

        public IEnumerable<IngredientDto> GetAllIngredients(string query = "")
        {
            var queryUpper = string.IsNullOrEmpty(query) ? null : query.ToUpper();

            var results = Context.Ingredients
                                 .Where(
                                            ing =>
                                            ing.IsValid
                                            &&
                                            (
                                                string.IsNullOrEmpty(queryUpper)
                                                ||
                                                ing.Name.ToUpper().Contains(queryUpper)
                                                ||
                                                ing.Description.ToUpper().Contains(queryUpper)
                                                ||
                                                ing.IngredientType.ToString().ToUpper().Contains(queryUpper)
                                            )
                                        );

            var dtoResults = results.Select(r => new IngredientDto()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IngredientType = r.IngredientType,
            }).AsEnumerable();

            return dtoResults;
        }

        public IEnumerable<IngredientDetailsDto> GetAllIngredientsWithDetails(string query = "")
        {
            var queryUpper = string.IsNullOrEmpty(query) ? null : query.ToUpper();

            var results = Context.Ingredients
                                 .Include(i => i.RecipeIngredients)
                                 .Where(
                                            ing =>
                                            ing.IsValid
                                            &&
                                            (
                                                string.IsNullOrEmpty(queryUpper)
                                                ||
                                                ing.Name.ToUpper().Contains(queryUpper)
                                                ||
                                                ing.Description.ToUpper().Contains(queryUpper)
                                                ||
                                                ing.IngredientType.ToString().ToUpper().Contains(queryUpper)
                                            )
                                        );

            var dtoResults = results.Select(r => new IngredientDetailsDto()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IngredientType = r.IngredientType,
                RecipesCount = r.RecipeIngredients.Count(ri => ri.IsValid),
            }).AsEnumerable();

            return dtoResults;
        }

        public IngredientDto GetIngredient(int id)
        {
            var result = GetSingleOrDefaultBaseEntity(id, true);

            var dtoResult = new IngredientDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                IngredientType = result.IngredientType,
            };

            return dtoResult;
        }

        public bool RemoveIngredient(int id)
        {
            return base.Remove(id);
        }

        public bool RemoveIngredientPermanently(int id)
        {
            return base.RemovePermanently(id);
        }

        public bool[] RemoveRangeIngredients(params int[] ids)
        {
            return base.RemoveRange(ids);
        }

        public bool[] RemoveRangeIngredientsPermanently(params int[] ids)
        {
            return base.RemoveRangePermanently(ids);
        }
    }
}
