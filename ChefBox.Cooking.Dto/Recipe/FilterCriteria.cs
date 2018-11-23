using ChefBox.Enum.Cooking.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipesFilterCriteria
    {
        public bool? IsPublished { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Query { get; set; }
        public int? CategoryId { get; set; }
        public RecipeType? RecipeType { get; set; }

        public int SkipAmount => PageSize * PageNumber;
    }
}
