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
    public class ToUserControlConverter : BaseValueConverter<ToUserControlConverter>
    {
        public override object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
        {
            switch((SideMenuControls)value)
            {
                case SideMenuControls.ChatList:
                    return new ChatListControl();
                case SideMenuControls.UserList:
                    return new UserListControl();
                case SideMenuControls.FileList:
                    return new FileListControl();
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
