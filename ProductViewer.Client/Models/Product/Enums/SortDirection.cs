using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductViewer.Client.Models.Product.Enums
{
    public enum SortDirection
    {
        [Display(Name = "Ascending")]
        Ascending = 1,

        [Display(Name = "Descending")]
        Descending = 2,
    }

    public static class SortDirectionDisplay
    {
        public static string? GetEnumAsString(SortDirection direction)
        {
            Type type = typeof(SortDirection);
            FieldInfo field = type.GetField(direction.ToString());

            if (field == null)
            {
                return null;
            }

            DisplayAttribute attr = field.GetCustomAttribute<DisplayAttribute>();
            if (attr == null)
            {
                return null;
            }
            return attr.Name;
        }

        public static SortDirection? GetStringAsEnum(string direction)
        {
            if (string.IsNullOrEmpty(direction))
            {
                return null;
            }

            switch (direction.ToLower())
            {
                case "ascending": return SortDirection.Ascending;
                case "descending": return SortDirection.Descending;
                default: return null;
            }
        }

        public static string[] DetFields()
        {
            FieldInfo[] fields = typeof(SortDirection).GetFields();
            string[] fieldNames = new string[fields.Length];
            
            for (int i = 0; i < fieldNames.Length; i++)
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