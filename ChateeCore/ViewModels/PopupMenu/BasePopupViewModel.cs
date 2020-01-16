using ChateeCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChateeCore
{
    public class BasePopupMenuViewModel : BaseViewModel
    {
        #region Public Properties
        public BaseViewModel Content { get; set; }
        public string BubbleBackgroundRGB { get; set; }
        public ElementHorizontalAlignment ArrowAlignment { get; set; }
        #endregion
        #region Constructors
        public BasePopupMenuViewModel()
        {
            BubbleBackgroundRGB = "ffffff";
            ArrowAlignment = ElementHorizontalAlignment.Left;
        }
        #endregion
    }
}
