using ChefBox.AdminUI.ViewModels.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToUl(this List<RecipeIngredientsViewModel> data)
        {
            var sb = new StringBuilder();
            sb.Append("<ul  class=\"list-group\">");
            foreach (var item in data)
            {
                sb.Append($"<li class=\"list-group-item\"><b>{item.IngredientName}</b> : {item.Amount} {item.Unit.ToString()}</li>");
            }
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}
