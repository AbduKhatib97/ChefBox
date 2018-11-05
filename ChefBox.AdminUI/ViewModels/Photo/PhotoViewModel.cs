using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.ViewModels.Photo
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        [Required]
        public string RecipeName { get; set; }
    }
}
