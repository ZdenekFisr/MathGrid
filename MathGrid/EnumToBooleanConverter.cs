using System.Globalization;
using System.Windows.Data;

namespace MathGrid
{
    /// <summary>
    /// Converts between an enum value and a boolean for use in WPF RadioButton binding.
    /// This is particularly useful when binding a group of RadioButtons to an enum.
    /// </summary>
    public class EnumToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts an enum value to a boolean, returning true if the value matches the parameter.
        /// </summary>
        /// <param name="value">The enum value to check.</param>
        /// <param name="targetType">The target type, expected to be a boolean.</param>
        /// <param name="parameter">The enum value to compare against.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>True if the value matches the parameter; otherwise, false.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(parameter) ?? false;
        }

        /// <summary>
        /// Converts a boolean back to the enum value, returning the parameter if the value is true.
        /// </summary>
        /// <param name="value">The boolean value indicating selection.</param>
        /// <param name="targetType">The target type, expected to be an enum type.</param>
        /// <param name="parameter">The enum value to return if the boolean is true.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>The parameter if the value is true; otherwise, returns Binding.DoNothing.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? parameter : Binding.DoNothing;
        }
    }
}
