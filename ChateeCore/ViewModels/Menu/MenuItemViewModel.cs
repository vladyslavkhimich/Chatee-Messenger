using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class MenuItemViewModel : BaseViewModel
    {
        #region Public Properties
        public string Text { get; set; }
        public IconType Icon { get; set; }
        public MenuItemType Type { get; set; }
        #endregion
        
    }
}
