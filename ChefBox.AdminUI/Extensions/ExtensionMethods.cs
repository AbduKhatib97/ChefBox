using ChefBox.AdminUI.ViewModels.Recipe;
using ChefBox.Enum.Cooking.Enums;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChefBox.AdminUI.Extensions
{
    public static class ExtensionMethods
    {
        public static string ToUl(this IEnumerable<RecipeIngredientsViewModel> data)
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

        public static string FromHtmlToInnerText(this string data)
        {
            return HtmlNode.CreateNode($"<html> {data} </html>").Element("p").InnerText.Replace("&nbsp;", " ");
        }
        public static string ToHtml(this string data)
        {
            return HtmlNode.CreateNode($"<div> {data} </div>").InnerHtml;
        }
        public static string ToIconClass(this Unit unit)
        {
            switch (unit)
            {
                case Unit.M:
                case Unit.Inch:
                case Unit.Length:
                case Unit.MM:
                case Unit.CM:
                    {
                        return "fas fa-ruler-vertical";
                    }
                case Unit.cup:
                case Unit.Ounce:
                    {
                        return "mdi mdi-cup";
                    }
                case Unit.Tablespoon:
                case Unit.Teaspoon:
                    {
                        return "fas fa-utensil-spoon";
                    }
                case Unit.Kg:
                case Unit.G:
                case Unit.Pound:
                    {
                        return "fas fa-weight-hanging";
                    }
                default:
                    {
                        return "fas fa-shapes";
                    }
            }
        }
        public static string ToIconClass(RecipeType recipeType)
        {
            switch (recipeType)
            {
                case RecipeType.Entrees:
                    {
                        return "fas fa-utensils";
                    }
                case RecipeType.MainDish:
                    {
                        return "mdi mdi-food";
                    }
                case RecipeType.Sweet:
                    {
                        return "fas fa-cookie";
                    }
                default:
                    {
                        return "mdi-food-variant";
                    }
            }
        }
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }


    }
}