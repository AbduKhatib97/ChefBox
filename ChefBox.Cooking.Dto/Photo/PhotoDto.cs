using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ChefBox.Cooking.Dto.Photo
{
    public class PhotoDto
    {
        public int Id { get; set; }
        [Display(Name = "Recipe Name")]
        public int RecipeId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
    }
}
