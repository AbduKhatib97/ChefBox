using ChefBox.Cooking.Dto.Ingredient;
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
    public class IngredientRepository : Base.BaseRepository, IIngredientRepository
    {
        public IngredientRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public IngredientDto ActionIngredient(IngredientDto ingredientDto)
        {
            try
            {
                var ingredientEntity = GetSingleOrDefaultBaseEntity<Ingredient>(ingredientDto.Id, isValid: true);
                EntityState entityState = EntityState.Modified;
                if (ingredientEntity == null)
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

        public IEnumerable<RecipeFormDto> GetAllIngredientRecipes(int ingredientId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IngredientDto> GetAllIngredients(string query = null)
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
                                        ).AsEnumerable();

            var dtoResults = results.Select(r => new IngredientDto()
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IngredientType = r.IngredientType,
            });

            return dtoResults;
        }

        public IngredientDto GetIngredient(int id)
        {
            var result = GetSingleOrDefaultBaseEntity<Ingredient>(id, true);
            var dtoResult = new IngredientDto()
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                IngredientType = result.IngredientType,
            };

            return dtoResult;
        }

        public int GetRecipesCount(int ingredientId)
        {
            var result = Context.RecipeIngredients
                                .Count(ri => ri.IngredientId == ingredientId && ri.IsValid);
            return result;
        }

        public bool RemoveIngredient(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveIngredientPermanently(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRangeIngredients(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public bool RemoveRangeIngredientsPermanently(params int[] ids)
        {
            throw new NotImplementedException();
        }
    }
}
