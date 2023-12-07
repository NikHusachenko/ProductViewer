using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductViewer.Client.Models.Product.Enums
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

            if (field == null)
            {
                return null;
            }

            DisplayAttribute attr = field.GetCustomAttribute<DisplayAttribute>();
            return attr?.Name;
        }

        public static SortType? GetStringAsEnum(string sortType)
        {
            switch (sortType.ToLower())
            {
                case "index": return SortType.Index;
                case "price": return SortType.Price;
                case "rate": return SortType.Rate;
                case "count": return SortType.Count;
                default: return null;
            }
        }

        public static string[] GetFields()
        {
            Type type = typeof(SortType);
            FieldInfo[] fields = type.GetFields();
            string[] fieldNames = new string[fields.Length];

            for (int i = 0; i < fields.Length; i++)
            {
                DisplayAttribute attr = fields[i].GetCustomAttribute<DisplayAttribute>();

                if (attr == null)
                {
                    fieldNames[i] = string.Empty;
                }
                else
                {
                    fieldNames[i] = attr.Name;
                }
            }

            return fieldNames;
        }
    }
}