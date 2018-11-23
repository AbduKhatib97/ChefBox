using ChefBox.AdminUI.ViewModels.Base;
using ChefBox.Enum.Cooking.Enums;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.AdminUI.ViewModels.Recipe
{
    public class RecipeViewModel : SharedViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name = "Type")]
        public RecipeType RecipeType { get; set; }
        [Display(Name = "Is Published")]
        public bool IsPublished { get; set; }
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}
