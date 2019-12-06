using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class MenuItemDesignModel : MenuItemViewModel
    {
        #region Singleton
        public static MenuItemDesignModel Instance => new MenuItemDesignModel();
        #endregion
        public MenuItemDesignModel()
        {
            Text = "Hello World!";
            Icon = IconType.File;
        }
    }
}
