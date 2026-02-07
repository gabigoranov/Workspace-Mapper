using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace WorkflowManager.Converters;

public class StringSelectConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool condition && parameter is string paramString)
        {
            var parts = paramString.Split('|');
            if (parts.Length == 2)
            {
                // Returns the first part if true, second part if false
                return condition ? parts[0] : parts[1];
            }
        }
        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) 
        => throw new NotSupportedException();
}