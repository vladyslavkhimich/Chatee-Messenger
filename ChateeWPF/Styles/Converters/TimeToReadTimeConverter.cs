using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChateeWPF
{
    public class TimeToReadTimeConverter : BaseValueConverter<TimeToReadTimeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (DateTime)value;
            if (time == DateTime.MinValue)
                return string.Empty;
            if (time.Date == DateTime.UtcNow.Date)
                return $"Read {time.ToLocalTime().ToString("HH:mm")}";
            return $"Read {time.ToLocalTime().ToString("HH:mm, MMM yyyy")}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
