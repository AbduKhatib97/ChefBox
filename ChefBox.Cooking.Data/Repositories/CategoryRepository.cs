using ChefBox.Cooking.Data.Repositories.Base;
using ChefBox.Cooking.Dto.Category;
using ChefBox.Cooking.IData.Interfaces;
using ChefBox.SqlServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChefBox.Cooking.Data.Repositories
{
    public class CategoryRepository : BaseRepository,ICategoryRepository
    {
        public CategoryRepository(ChefBoxDbContext context) : base(context)
        {
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var results = Context.Categories
                                 .Where(c => c.IsValid)
                                 .AsEnumerable();

            var dtoResults = results.Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
            });

            return dtoResults;
        }
    }
}
