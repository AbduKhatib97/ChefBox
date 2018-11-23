using ChefBox.Cooking.Dto.Photo;
using ChefBox.Cooking.Dto.Recipe;
using System.Collections.Generic;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface IPhotoRepository
    {
        IEnumerable<PhotoDto> GetAllPhotos(string query = "");
        PhotoDto GetPhoto(int id);
        PhotoDto GetPhoto(string url);
        PhotoDto ActionPhoto(PhotoDto photoDto);
        bool RemovePhoto(int id);
        bool[] RemoveRangePhotos(params int[] ids);
        bool RemovePhotoPermanently(int id);
        bool[] RemoveRangePhotosPermanently(params int[] ids);
        RecipeDto GetRecipe(int photoId);
        string GetRecipeName(int photoId);
    }
}
