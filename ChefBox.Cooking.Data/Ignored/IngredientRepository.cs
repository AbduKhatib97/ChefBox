//using ChefBox.Cooking.Data.Repositories.Base;
//using ChefBox.Cooking.Dto.Ingredient;
//using ChefBox.Cooking.Dto.Recipe;
//using ChefBox.Cooking.IData.Interfaces;
//using ChefBox.Model.Cooking;
//using ChefBox.SqlServer.Database;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace ChefBox.Cooking.Data.Repositories1
//{
//    public class IngredientRepository : BaseRepository, IIngredientRepository
//    {
//        public IngredientRepository(ChefBoxDbContext context) : base(context)
//        {
//            Context = context;
//        }

//        public IEnumerable<IngredientDto> GetAllIngredients(string query = null)
//        {
//            var queryUpper = string.IsNullOrEmpty(query) ? null : query.ToUpper();

//            var results = Context.Ingredients
//                                 .Where(
//                                         ing =>
//                                         ing.IsValid
//                                         &&
//                                         (
//                                             string.IsNullOrEmpty(queryUpper)
//                                             ||
//                                             ing.Name.ToUpper().Contains(queryUpper)
//                                             ||
//                                             ing.Description.ToUpper().Contains(queryUpper)
//                                             ||
//                                             ing.IngredientType.ToString().ToUpper().Contains(queryUpper)
//                                         )
//                                     )
//                                 .Include(ing => ing.RecipeIngredients)
//                                 .Select(ing => new IngredientDto()
//                                 {
//                                     Id = ing.Id,
//                                     Name = ing.Name,
//                                     Description = ing.Description,
//                                     IngredientType = ing.IngredientType,
//                                     RecipesCount = ing.RecipeIngredients.Count(ri => ri.IsValid),
//                                 })
//                                 .ToList();
//            return results;
//        } // Done

//        public IngredientDto ActionIngredient(IngredientDto ingredientDto) // Done
//        {
//            try
//            {
//                var ingredientEntity = GetSingleOrDefaultBaseEntity<Ingredient>(ingredientDto.Id, isValid: true);
//                EntityState entityState = EntityState.Modified;
//                if (ingredientEntity == null)
//                {
//                    ingredientEntity = new Ingredient()
//                    {
//                        Name = ingredientDto.Name,
//                        Description = ingredientDto.Description,
//                        IngredientType = ingredientDto.IngredientType,
//                    };
//                    entityState = EntityState.Added;
//                }//Add case
//                else
//                {
//                    ingredientEntity.Name = ingredientDto.Name;
//                    ingredientEntity.Description = ingredientDto.Description;
//                    ingredientEntity.IngredientType = ingredientDto.IngredientType;
//                }//Update case
//                Context.Entry(ingredientEntity).State = entityState;
//                Context.SaveChanges();
//                ingredientDto.Id = ingredientEntity.Id;
//                return ingredientDto;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }

//        public bool RemoveIngredient(int id)
//        {
//            try
//            {
//                var ingredientEntity = GetSingleOrDefaultBaseEntity<Ingredient>(id, isValid: true);
//                if (ingredientEntity != null)
//                {
//                    ingredientEntity.IsValid = false;
//                    Context.Update(ingredientEntity);
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public IngredientDto GetIngredient(int id)
//        {
//            var ingredientEntity = GetSingleOrDefaultBaseEntity<Ingredient>(id, isValid: true);
//            var result = ingredientEntity is null ? null : new IngredientDto()
//            {
//                Id = ingredientEntity.Id,
//                Name = ingredientEntity.Name,
//                Description = ingredientEntity.Description,
//                IngredientType = ingredientEntity.IngredientType,
//                RecipesCount = ingredientEntity.RecipeIngredients.Count(ri => ri.IsValid),
//            };
//            return result;
//        } // Done


//        public bool RemoveRangeIngredients(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemoveIngredient(id);
//                    if (!isDeleted)
//                    {
//                        throw new Exception();
//                    }

//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public bool RemoveIngredientPermanently(int id)
//        {
//            try
//            {
//                var ingredientEntity = GetSingleOrDefaultBaseEntity<Ingredient>(id, isValid: true);
//                if (ingredientEntity != null)
//                {
//                    Context.Entry(ingredientEntity).State = EntityState.Deleted;
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public bool RemoveRangeIngredientsPermanently(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemoveIngredientPermanently(id);
//                    if (!isDeleted)
//                    {
//                        throw new Exception();
//                    }

//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public IEnumerable<RecipeFormDto> GetAllIngredientRecipes(bool? isPublished, int ingredientId)
//        {
//            var results = Context.RecipeIngredients
//                                 .Include(ri => ri.Recipe)
//                                 .Where(
//                                            ri =>
//                                            ri.IsValid
//                                            &&
//                                            ri.IngredientId == ingredientId
//                                            &&
//                                            (isPublished.HasValue ? ri.Recipe.IsPublished == isPublished.Value : true)
//                                       )
//                                 .Select(ri => new RecipeFormDto()
//                                 {
//                                     Id = ri.Recipe.Id,
//                                     Name = ri.Recipe.Name,
//                                     Description = ri.Recipe.Description,
//                                     RecipeType = ri.Recipe.RecipeType,
//                                     IsPublished = ri.Recipe.IsPublished,
//                                     CategoryId = ri.Recipe.CategoryId,
//                                     RecipeIngredients = (new RecipeRepository(Context)).GetAllRecipeIngredients(ri.Id).Result,
//                                     Photos = (new RecipeRepository(Context)).GetAllRecipePhotos(ri.Id).Result,

//                                 }).ToList();
//            return results;

//        } // Done
//    }
//}
