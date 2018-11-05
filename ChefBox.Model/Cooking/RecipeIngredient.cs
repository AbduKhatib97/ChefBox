using ChefBox.Enum.Cooking.Enums;
using ChefBox.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChefBox.Model.Cooking
{
    [Table("RecipeIngredients", Schema = "Cooking")]
    public class RecipeIngredient : BaseEntity<int>
    {
        public double Amount { get; set; }
        [Required]
        public Unit Unit { get; set; }

        #region Forign Keys
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }

        #endregion
    }
}
