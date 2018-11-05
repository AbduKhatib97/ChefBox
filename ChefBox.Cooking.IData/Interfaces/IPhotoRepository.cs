using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IPhotoRepository
    {
        IEnumerable<PhotoDto> GetAllPhotos(string query = null);
        PhotoDto GetPhoto(int id);
        PhotoDto GetPhoto(string url);
        PhotoDto ActionPhoto(PhotoDto photoDto);
        bool RemovePhoto(int id);
        bool RemoveRangePhotos(params int[] ids);
        bool RemovePhotoPermanently(int id);
        bool RemoveRangePhotosPermanently(params int[] ids);
        RecipeFormDto GetRecipe(int photoId);
    }
}
