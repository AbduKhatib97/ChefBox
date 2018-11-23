
using ChefBox.Cooking.Dto.Shared;
using ChefBox.Cooking.Dto.Statistics;

namespace ChefBox.Cooking.IData.Interfaces
{
    public interface ISharedRepository
    {
        SharedDto GetSharedContent();
        int GetRecipesCount();
        int GetIngredientsCount();
        int GetCategoriesCount();
        int GetPhotosCount();
        StatisicsDto GetStatisics();
    }
}
