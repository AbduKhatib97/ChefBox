using ChefBox.Enum.Cooking.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeIngredientDto
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public Unit Unit { get; set; }
        public double Amount { get; set; }
        [Display(Name = "Is Checked")]
        public bool IsChecked { get; set; }
        [Required]
        public string IngredientName { get; set; }
    }
}
