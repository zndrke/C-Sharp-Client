using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace WpfApplication2
{
    public class CommaConverter : IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            string sStr = value as String;

            double dResult;

            if (string.IsNullOrEmpty(sStr)) {
                return value;
            }

            if (!double.TryParse(sStr, out dResult)) {
                MessageBox.Show(sStr + " 파싱 불가! ");
                ;

            } else {
                return dResult.ToString("#,##0.##" + "%");
            }
            return "";
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }
}
