using ChefBox.Enum.Cooking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        [Display(Name = "Recipe Type")]
        public RecipeType RecipeType { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Is Published")]
        public bool IsPublished { get; set; }

    }
}
