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
                new MenuItemViewModel { Type = MenuItemType.Header, Text="Attach a file ..." },
                new MenuItemViewModel { Icon = IconType.File , Text="From computer" },
                new MenuItemViewModel { Icon = IconType.Picture , Text="From pictures" }
            };
        }
        #endregion
    }
}
