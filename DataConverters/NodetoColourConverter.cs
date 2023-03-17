using RoutingProjectNet.src;
using System.Globalization;
using System.Diagnostics;

namespace RoutingProjectNet.DataConverters
{
    public class NodetoColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Node original = (Node)value;
            if (original == null)
            {
                return Colors.Purple;
            }
            NodeStatus status = original.GetStatus();
            if (status == NodeStatus.Free)
            {
                return Colors.Blue;
            }
            else if (status == NodeStatus.Obstacle)
            {
                return Colors.Red;
            }
            else if (status == NodeStatus.Start)
            {
                return Colors.Green;
            }
            else if (status == NodeStatus.End)
            {
                return Colors.Yellow;
            }
            return Colors.White;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class NodeToIncludedColourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Node original = (Node)value;
            if (original == null)
            {
                return Colors.Transparent;
            }
            if (original.GetIncluded())
            {
                return Colors.Orange;
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
