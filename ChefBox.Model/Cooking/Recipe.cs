using ChefBox.Enum.Cooking.Enums;
using ChefBox.Model.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefBox.Model.Cooking
{
    [Table("Recipes", Schema = "Cooking")]
    public class Recipe : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public RecipeType RecipeType { get; set; }
        public bool IsPublished { get; set; }

        #region Forign Keys
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        #endregion

        #region Navigation Properties
        public ICollection<Photo> Photos { get; set; }
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }
        
        #endregion
    }
}
