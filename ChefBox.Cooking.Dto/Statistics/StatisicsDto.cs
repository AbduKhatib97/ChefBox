using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.Dto.Statistics
{
    public class StatisicsDto
    {
        public EntityStatisticsDto RecipeStatistics { get; set; }
        public EntityStatisticsDto CategoryStatistics { get; set; }
        public EntityStatisticsDto IngredientStatistics { get; set; }
        public EntityStatisticsDto PhotoStatistics { get; set; }
    }
}
