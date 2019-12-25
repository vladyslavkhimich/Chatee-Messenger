using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class MenuDesignModel : MenuViewModel
    {
        #region Singleton
        public static MenuDesignModel Instance => new MenuDesignModel();
        #endregion
        #region Constructors
        public MenuDesignModel()
        {
            Items = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Type = MenuItemType.Header, Text="Design time header ..." },
                new MenuItemViewModel { Icon = IconType.File , Text="Menu item 1" },
                new MenuItemViewModel { Icon = IconType.Picture , Text="Menu item 2" }
            };
        }
        #endregion
    }
}
