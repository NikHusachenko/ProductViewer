﻿using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ProductViewer.Services.ProductService.Models.Query
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
            return attr?.Name;
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
    }
}