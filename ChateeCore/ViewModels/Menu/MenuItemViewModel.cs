using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class MenuItemViewModel : BaseViewModel
    {
        #region Public Properties
        public string Text { get; set; }
        public IconType Icon { get; set; }
        public MenuItemType Type { get; set; }
        public ICommand Command { get; set; }
        #endregion
        
    }
}
