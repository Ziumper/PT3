using PT3.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PT3.DialogWindow
{
    public class DirectionBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = Int32.Parse((string)parameter);
            Direction enumValue = (Direction)intValue;

            if (value.Equals(enumValue))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false) return Direction.Ascending;
            return (Direction)parameter;
        }
    }
}
