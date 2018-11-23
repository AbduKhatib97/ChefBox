using System.ComponentModel.DataAnnotations;

namespace ChefBox.Cooking.Dto.Photo
{
    public class PhotoDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string RecipeName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public bool IsCover { get; set; }
    }
}
