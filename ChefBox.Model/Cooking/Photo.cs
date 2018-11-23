using ChefBox.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefBox.Model.Cooking
{
    [Table("Photos", Schema = "Cooking")]
    public class Photo : BaseEntity<int>
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Url]
        public string Url { get; set; }
        public bool IsCover { get; set; }
        #region Forign Keys
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        #endregion
    }
}
