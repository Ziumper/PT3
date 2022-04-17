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
    public class SortByBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int intValue = Int32.Parse((string)parameter);
            SortBy enumValue = (SortBy)intValue;
       
            if (value.Equals(enumValue))
                return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == false) return null;
            return (SortBy)parameter;
        }

        //TODO Add converting values
        //private SortBy co
    }
}
