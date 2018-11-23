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
    public class PhotoRepository : BaseCookingRepository<Photo,int>, IPhotoRepository
    {
        public PhotoRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public PhotoDto ActionPhoto(PhotoDto photoDto)
        {
            try
            {
                var photoEntity = GetSingleOrDefaultBaseEntity(photoDto.Id, isValid: true);
                EntityState entityState = EntityState.Modified;
                if (photoEntity is null)
                {
                    photoEntity = new Photo()
                    {
                        Name = photoDto.Name,
                        Url = photoDto.Url,
                        RecipeId = photoDto.RecipeId,
                    };
                    entityState = EntityState.Added;
                } // Add case
                else
                {
                    photoEntity.Name = photoDto.Name;
                    photoEntity.Url = photoDto.Url;
                    photoEntity.RecipeId = photoDto.RecipeId;
                } // Update case
                Context.Entry(photoEntity).State = entityState;
                Context.SaveChanges();
                photoDto.Id = photoEntity.Id;
                return photoDto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<PhotoDto> GetAllPhotos(string query = "")
        {
            var Query = string.IsNullOrEmpty(query) ? null : query.ToUpper();

            var results = Context.Photos
                                 .Include(ph => ph.Recipe)
                                 .Where(ph =>
                                            ph.IsValid
                                            &&
                                            (
                                                string.IsNullOrEmpty(Query)
                                                ||
                                                ph.Name.ToUpper().Contains(Query)
                                            )
                                       ).AsEnumerable();

            var dtoResults = results.Select(ph => new PhotoDto()
                                     {
                                         Id = ph.Id,
                                         Name = ph.Name,
                                         Url = ph.Url,
                                         RecipeId = ph.RecipeId,
                                         RecipeName = ph.Recipe.Name,
                                     });
            return dtoResults;
        }

        public PhotoDto GetPhoto(int id)
        {
            var result = GetSingleOrDefaultBaseEntity(id, isValid: true);
            var recipeName = GetRecipeName(id);

            var dtoResult = result is null ? null : new PhotoDto()
            {
                Id = result.Id,
                Name = result.Name,
                Url = result.Url,
                RecipeId = result.RecipeId,
                RecipeName = recipeName,
            };
            return dtoResult;
        }

        public PhotoDto GetPhoto(string url)
        {
            var result = Context.Photos.SingleOrDefault(r => r.Url == url && r.IsValid);
            var recipeName = GetRecipeName(result.Id);

            var dtoResult = result is null ? null : new PhotoDto()
            {
                Id = result.Id,
                Name = result.Name,
                Url = result.Url,
                RecipeId = result.RecipeId,
                RecipeName = recipeName,
            };
            return dtoResult;
        }

        public RecipeDto GetRecipe(int photoId)
        {
            var result = GetSingleOrDefaultBaseEntity(photoId, isValid: true);
            var dtoResult = result is null ? null : new RecipeRepository(Context).GetRecipe(result.RecipeId);
            return dtoResult;
        }

        public string GetRecipeName(int photoId)
        {
            var result = Context.Photos
                                .Include(ph => ph.Recipe)
                                .SingleOrDefault(ph => ph.IsValid && ph.Id == photoId)
                                .Recipe.Name;
            return result;
        }

        public bool RemovePhoto(int id)
        {
            return base.Remove(id);
        }

        public bool RemovePhotoPermanently(int id)
        {
            return base.RemovePermanently(id);
        }

        public bool[] RemoveRangePhotos(params int[] ids)
        {
            return base.RemoveRange(ids);
        }

        public bool[] RemoveRangePhotosPermanently(params int[] ids)
        {
            return base.RemoveRangePermanently(ids);
        }


    }
}
