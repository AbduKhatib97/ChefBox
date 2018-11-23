using ChefBox.AdminUI.ViewModels.Base;
using System.ComponentModel.DataAnnotations;

namespace ChefBox.AdminUI.ViewModels.Photo
{
    public class PhotoViewModel : SharedViewModel
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public string Name { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
    }
}
