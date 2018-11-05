using ChefBox.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChefBox.Model.Cooking
{
    [Table("Photos", Schema = "Cooking")]
    public class Photo : BaseEntity<int>
    {
        [Required]
        public string Description { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }

        #region Forign Keys
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        #endregion
    }
}
