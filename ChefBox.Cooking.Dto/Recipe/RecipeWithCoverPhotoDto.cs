using ChefBox.Cooking.Dto.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChefBox.Cooking.Dto.Recipe
{
    public class RecipeWithCoverPhotoDto : RecipeDto
    {
        public PhotoDto CoverPhoto { get; set; }
    }
}
