using ChefBox.Cooking.Dto.Photo;
using ChefBox.Enum.Cooking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeFormDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Recipe Type")]
        public RecipeType RecipeType { get; set; }
        public string Description { get; set; }
        public IEnumerable<RecipeIngredientDto> RecipeIngredients { get; set; }
        public IEnumerable<PhotoDto> Photos { get; set; }
        [Required]
        [Display(Name = "Is Published")]
        public bool? IsPublished { get; set; }
    }

}