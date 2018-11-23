using ChefBox.Enum.Cooking.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        [Required]
        public RecipeType RecipeType { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsPublished { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
