using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.Enum.Cooking.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipeIngredientsViewModel : SharedViewModel
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        [Required]
        [Display(Name ="Ingredient Name")]
        public string IngredientName { get; set; }
        public double Amount { get; set; }
        [Required]
        public Unit Unit { get; set; }
    }
}