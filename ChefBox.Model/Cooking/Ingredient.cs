using ChefBox.Enum.Cooking.Enums;
using ChefBox.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChefBox.Model.Cooking
{
    [Table("Ingredients", Schema = "Cooking")]
    public class Ingredient : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IngredientType  IngredientType { get; set; }
        public string Description { get; set; }

        #region Navigation Properties
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        #endregion
    }
}
