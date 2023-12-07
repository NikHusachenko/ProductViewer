using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductViewer.Services.ProductService.Models.Query
{
    public enum SortType
    {
        [Display(Name = "Index")]
        Index = 1,

        [Display(Name = "Price")]
        Price = 2,

        [Display(Name = "Rate")]
        Rate = 3,

        [Display(Name = "Count")]
        Count = 4,
    }

    public static class SortTypeDisplay
    {
        public static string? GetEnumAsString(SortType sortType)
        {
            Type type = typeof(SortType);
            FieldInfo field = type.GetField(sortType.ToString());

            if (field  == null)
            {
                return null;
            }

            DisplayAttribute attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name;
        }

        public static SortType? GetStringAsEnum(string sortType)
        {
            if (string.IsNullOrEmpty(sortType))
            {
                return null;
            }

            switch (sortType.ToLower())
            {
                case "index": return SortType.Index;
                case "price": return SortType.Price;
                case "rate": return SortType.Rate;
                case "count": return SortType.Count;
                default: return null;
            }
        }
    }
}