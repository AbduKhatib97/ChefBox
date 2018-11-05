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

//namespace ChefBox.Cooking.Data.Repositories
//{
//    public class PhotoRepository : BaseRepository, IPhotoRepository
//    {
//        public PhotoRepository(ChefBoxDbContext context) : base(context)
//        {
//            Context = context;
//        }

//        #region IPhotoRepository Implementation

//        public PhotoDto ActionPhoto(PhotoDto photoDto)
//        {
//            try
//            {
//                var photoEntity = GetSingleOrDefaultBaseEntity<Photo>(photoDto.Id, isValid: true);
//                EntityState entityState = EntityState.Modified;
//                if (photoEntity == null)
//                {
//                    photoEntity = new Photo()
//                    {
//                        Description = photoDto.Description,
//                        Url = photoDto.Url,
//                        RecipeId = photoDto.RecipeId
//                    };
//                    entityState = EntityState.Added;
//                } // Add case
//                else
//                {
//                    photoEntity.Description = photoDto.Description;
//                    photoEntity.Url = photoDto.Url;
//                    photoEntity.RecipeId = photoDto.RecipeId;
//                } // Update case
//                Context.Entry(photoEntity).State = entityState;
//                Context.SaveChanges();
//                photoDto.Id = photoEntity.Id;
//                return photoDto;
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        } // Done

//        public IEnumerable<PhotoDto> GetAllPhotos(string query = null)
//        {
//            var Query = string.IsNullOrEmpty(query) ? null : query.ToUpper();

//            var results = Context.Photos
//                                 .Where(ph =>
//                                            ph.IsValid
//                                            &&
//                                            (
//                                                string.IsNullOrEmpty(Query)
//                                                ||
//                                                ph.Description.ToUpper().Contains(Query)
//                                            )
//                                       )
//                                 .Select(ph => new PhotoDto()
//                                 {
//                                     Id = ph.Id,
//                                     Description = ph.Description,
//                                     Url = ph.Url,
//                                     RecipeId = ph.RecipeId,
//                                 }).ToList();
//            return results;
//        } // Done

//        public PhotoDto GetPhoto(int id)
//        {
//            var photoEntity = GetSingleOrDefaultBaseEntity<Photo>(id, isValid: true);
//            var result = photoEntity is null ? null : new PhotoDto()
//            {
//                Id = photoEntity.Id,
//                Description = photoEntity.Description,
//                Url = photoEntity.Url,
//                RecipeId = photoEntity.RecipeId,
//            };
//            return result;
//        } // Done

//        public PhotoDto GetPhoto(string url)
//        {
//            var photoEntity = Context.Photos.SingleOrDefault(r => r.Url == url && r.IsValid);
//            var result = photoEntity is null ? null : new PhotoDto()
//            {
//                Id = photoEntity.Id,
//                Description = photoEntity.Description,
//                Url = photoEntity.Url,
//                RecipeId = photoEntity.RecipeId,
//            };
//            return result;
//        } // Done

//        public RecipeFormDto GetRecipe(int photoId)
//        {
//            var photoEntity = GetSingleOrDefaultBaseEntity<Photo>(photoId, isValid: true);
//            var result = photoEntity is null ? null : (new RecipeRepository(Context)).GetRecipe(photoEntity.RecipeId);
//            return result;
//        } // Done

//        public bool RemovePhoto(int id)
//        {
//            try
//            {
//                var photoEntity = GetSingleOrDefaultBaseEntity<Photo>(id, isValid: true);
//                if (photoEntity != null)
//                {
//                    photoEntity.IsValid = false;
//                    Context.Update(photoEntity);
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public bool RemovePhotoPermanently(int id)
//        {
//            try
//            {
//                var photoEntity = GetSingleOrDefaultBaseEntity<Photo>(id, isValid: true);
//                if (photoEntity != null)
//                {
//                    Context.Entry(photoEntity).State = EntityState.Deleted;
//                    Context.SaveChanges();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        } // Done

//        public bool RemoveRangePhotos(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemovePhoto(id);
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

//        public bool RemoveRangePhotosPermanently(params int[] ids)
//        {
//            try
//            {
//                foreach (var id in ids)
//                {
//                    var isDeleted = RemovePhotoPermanently(id);
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


//        #endregion
//    }
//}
