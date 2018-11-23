using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.Enum.Cooking.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.AdminUI.ViewModels.Ingredient
{
    public class IngredientViewModel : SharedViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name ="Type")]
        public IngredientType IngredientType { get; set; }
    }
}
