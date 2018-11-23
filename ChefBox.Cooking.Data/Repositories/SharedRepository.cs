using ChefBox.Cooking.Dto.Shared;
using ChefBox.Cooking.Dto.Statistics;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.SqlServer.Database;
using System.Linq;

namespace ChefBox.Cooking.Data.Repositories
{
    public class SharedRepository : ISharedRepository
    {
        public ChefBoxDbContext Context { get; set; }
        public SharedRepository(ChefBoxDbContext context)
        {
            Context = context;
        }

        public int GetCategoriesCount()
        {
            return Context.Categories.Where(e => e.IsValid).Count();
        }

        public int GetIngredientsCount()
        {
            return Context.Ingredients.Where(e => e.IsValid).Count();
        }

        public int GetPhotosCount()
        {
            return Context.Photos.Where(e => e.IsValid).Count();
        }

        public int GetRecipesCount()
        {
            return Context.Recipes.Where(e => e.IsValid).Count();
        }

        public SharedDto GetSharedContent()
        {
            var dtoResult = new SharedDto()
            {
                CategoriesCount = GetCategoriesCount(),
                IngredientsCount = GetIngredientsCount(),
                RecipesCount = GetRecipesCount(),
                PhotosCount = GetPhotosCount(),
            };
            return dtoResult;
        }

        public StatisicsDto GetStatisics()
        {
            var latestCategory = Context.Categories
                .OrderBy(cat => cat.CreationDate)
                .FirstOrDefault();
            var latestIngredient = Context.Ingredients
               .OrderBy(ing => ing.CreationDate)
               .FirstOrDefault();
            var latestRecipe = Context.Recipes
               .OrderBy(rec => rec.CreationDate)
               .FirstOrDefault();
            var latestPhoto = Context.Photos
               .OrderBy(rec => rec.CreationDate)
               .FirstOrDefault();

            return new StatisicsDto()
            {
                RecipeStatistics = new EntityStatisticsDto()
                {
                    LatestItemId = latestRecipe == null ? 0 : latestRecipe.Id,
                    LatestItemName = latestRecipe == null ? "" : latestRecipe.Name,
                    EntitiesCount = Context.Recipes.Count(rec => rec.IsValid)
                },
                IngredientStatistics = new EntityStatisticsDto()
                {
                    LatestItemId = latestIngredient == null ? 0 : latestIngredient.Id,
                    LatestItemName = latestIngredient == null ? "" : latestIngredient.Name,
                    EntitiesCount = Context.Ingredients.Count(ing => ing.IsValid)
                },
                CategoryStatistics = new EntityStatisticsDto()
                {
                    LatestItemId = latestCategory == null ? 0 : latestCategory.Id,
                    LatestItemName = latestCategory == null ? "" : latestCategory.Name,
                    EntitiesCount = Context.Categories.Count(cat => cat.IsValid)
                },
                PhotoStatistics = new EntityStatisticsDto()
                {
                    LatestItemId = latestPhoto == null ? 0 : latestPhoto.Id,
                    LatestItemName = "",
                    EntitiesCount = Context.Photos.Count(cat => cat.IsValid)
                },
            };
        }
    }
}
