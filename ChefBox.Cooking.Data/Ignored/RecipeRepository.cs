//using ChefBox.Cooking.Data.Repositories.Base;
//using ChefBox.Cooking.Dto.Photo;
//using ChefBox.Cooking.Dto.Recipe;
//using ChefBox.Cooking.IData.Interfaces;
//using ChefBox.Model.Cooking;
//using ChefBox.SqlServer.Database;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ChefBox.Cooking.Data.Repositories1
//{
//    public class RecipeRepository : BaseRepository, IRecipeRepository
//    {
//        public RecipeRepository(ChefBoxDbContext context) : base(context)
//        {
//        }

//        #region IRecipeRepository Implementation

//        public async Task<RecipeFormDto> GetRecipe(int id)
//        {
//            var recipeEntity = await GetSingleOrDefaultBaseEntityAsync<Recipe>(id, isValid: true);
//            var result = recipeEntity is null ? null : new RecipeFormDto()
//            {
//                Id = recipeEntity.Id,
//                Name = recipeEntity.Name,
//                Description = recipeEntity.Description,
//                RecipeType = recipeEntity.RecipeType,
//                CategoryId = recipeEntity.CategoryId,
//                IsPublished = recipeEntity.IsPublished,
//                Photos = GetAllRecipePhotos(recipeEntity.Id).Result,
//                RecipeIngredients = GetAllRecipeIngredients(recipeEntity.Id).Result,
//            };
//            return result;
//        } //Done

//        public async Task<IEnumerable<RecipeFormDto>> GetAllRecipes(bool? isPublished, string query = "")
//        {
//            var queryUpper = string.IsNullOrEmpty(query) ? "" : query.ToUpper();

//            var results = await Context.Recipes
//                                 .Where(rec =>
//                                            rec.IsValid
//                                            &&
//                                            (isPublished.HasValue ? rec.IsPublished == isPublished : true)
//                                            &&
//                                            (
//                                                string.IsNullOrEmpty(queryUpper)
//                                                ||
//                                                rec.Name.ToUpper().Contains(queryUpper)
//                                                ||
//                                                rec.Description.ToUpper().Contains(queryUpper)
//                                                ||
//                                                rec.RecipeType.ToString().ToUpper().Contains(queryUpper)
//                                            )
//                                       )
//                                 .Select(rec => new RecipeFormDto()
//                                 {
//                                     Id = rec.Id,
//                                     Name = rec.Name,
//                                     Description = rec.Description,
//                                     RecipeType = rec.RecipeType,
//                                     IsPublished = rec.IsPublished,
//                                     CategoryId = rec.CategoryId,
//                                     RecipeIngredients = GetAllRecipeIngredients(rec.Id).Result,
//                                     Photos = GetAllRecipePhotos(rec.Id).Result
//                                 }).ToListAsync();
//            return results;
//        } //Done

//        public RecipeFormDto ActionRecipe(RecipeFormDto recipeFormDto)
//        {
//            try
//            {
//                var recipeEntity = GetSingleOrDefaultBaseEntity<Recipe>(recipeFormDto.Id, isValid: true);
//                EntityState entityState = EntityState.Modified;
//                if (recipeEntity == null)
//                {
//                    recipeEntity = new Recipe()
//                    {
//                        Name = recipeFormDto.Name,
//                        Description = recipeFormDto.Description,
//                        RecipeType = recipeFormDto.RecipeType,
//                        CategoryId = recipeFormDto.CategoryId,
//                        IsPublished = recipeFormDto.IsPublished.Value,
//                        Photos = recipeFormDto.Photos
//                                              .Select(phDto => new Photo()
//                                              {
//                                                  Description = phDto.Description,
//                                                  Url = phDto.Url,
//                                                  RecipeId = phDto.RecipeId,
//                                              }).ToList(),
//                        RecipeIngredients = recipeFormDto.RecipeIngredients
//                                                         .Select(riDto => new RecipeIngredient()
//                                                         {
//                                                             IngredientId = riDto.IngredientId,
//                                                             Amount = riDto.Amount,
//                                                             Unit = riDto.Unit,
//                                                         }).ToList(),
//                    };
//                    entityState = EntityState.Added;
//                } // Add case
//                else
//                {
//                    recipeEntity.Name = recipeFormDto.Name;
//                    recipeEntity.Description = recipeFormDto.Description;
//                    recipeEntity.RecipeType = recipeFormDto.RecipeType;
//                    recipeEntity.CategoryId = recipeFormDto.CategoryId;
//                    recipeEntity.IsPublished = recipeFormDto.IsPublished.Value;
//                    recipeEntity.Photos = recipeFormDto.Photos
//                                                       .Select(phDto => new Photo()
//                                                       {
//                                                           Description = phDto.Description,
//                                                           Url = phDto.Url,
//                                                           RecipeId = phDto.RecipeId,
//                                                       }).ToList();
//                    recipeEntity.RecipeIngredients = recipeFormDto.RecipeIngredients
//                                                                  .Select(riDto => new RecipeIngredient()
//                                                                  {
//                                                                      IngredientId = riDto.IngredientId,
//                                                                      Amount = riDto.Amount,
//                                                                      Unit = riDto.Unit,
//                                                                  }).ToList();
//                } // Update case
//                Context.Entry(recipeEntity).State = entityState;
//                Context.SaveChanges();
//                recipeFormDto.Id = recipeEntity.Id;
//                return recipeFormDto;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        } // Done

//        public bool RemoveRecipe(int id)
//        {
//            try
//            {
//                var recipeEntity = GetSingleOrDefaultBaseEntity<Recipe>(id, isValid: true);
//                if (recipeEntity != null)
//                {
//                    recipeEntity.IsValid = false;
//                    Context.Update(recipeEntity);
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } //Done

//        public bool RemoveRangeRecipes(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemoveRecipe(id);
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
//        } //Done

//        public async Task<IEnumerable<PhotoDto>> GetAllRecipePhotos(int recipeId)
//        {
//            var results = await Context.Photos.Where(ph => ph.RecipeId == recipeId && ph.IsValid)
//                                        .Select(ph => new PhotoDto()
//                                        {
//                                            Id = ph.Id,
//                                            RecipeId = ph.RecipeId,
//                                            Description = ph.Description,
//                                            Url = ph.Url,
//                                        }).ToListAsync();
//            return results;
//        } //Done

//        public async Task<IEnumerable<RecipeIngredientDto>> GetAllRecipeIngredients(int recipeId)
//        {
//            var results = await Context.RecipeIngredients.Where(ri => ri.RecipeId == recipeId && ri.IsValid)
//                                                   .Include(ri => ri.Ingredient)
//                                                   .Select(ri => new RecipeIngredientDto()
//                                                   {
//                                                       IngredientId = ri.IngredientId,
//                                                       Name = ri.Ingredient.Name,
//                                                       Amount = ri.Amount,
//                                                       Unit = ri.Unit,
//                                                       IsChecked = true,
//                                                   }).ToListAsync();
//            return results;
//        } //Done

//        public bool RemoveRecipePermanently(int id)
//        {
//            try
//            {
//                var recipeEntity = GetSingleOrDefaultBaseEntity<Recipe>(id, isValid: true);
//                if (recipeEntity != null)
//                {
//                    Context.Entry(recipeEntity).State = EntityState.Deleted;
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }

//        } //Done

//        public bool RemoveRangeRecipesPermanently(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemoveRecipePermanently(id);
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
//        } //Done


//        #endregion
//    }
//}
