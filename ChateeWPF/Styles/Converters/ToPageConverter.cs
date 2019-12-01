using ChateeCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeWPF
{
    public class ToPageConverter : BaseValueConverter<ToPageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((ApplicationPages)value)
            {
                case ApplicationPages.LoginPage:
                    return new LoginPage();
                case ApplicationPages.ChatPage:
                    return new ChatPage();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
