using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public static class IconTypeExtensions
    {
        public static string ToFontAwesome(this IconType iconType)
        {
            switch(iconType)
            {
                case IconType.File:
                    return "\uf0f6";
                case IconType.Picture:
                    return "\uf1c5";
                default:
                    return null;
            }
        }
    }
}
