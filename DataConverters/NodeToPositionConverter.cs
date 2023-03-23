
using Microsoft.Maui.Controls;
using RoutingProjectNet.src;
using System.Diagnostics;
using System.Globalization;

namespace RoutingProjectNet.DataConverters
{
    public class NodeToXPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {  
            Node original = (Node)value;
            if (original == null)
            {
                return 0;
            }
            return original.GetCoords().x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class NodeToYPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Node original = (Node)value;
            if (original == null)
            {
                return 0;
            }
            return original.GetCoords().y;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
