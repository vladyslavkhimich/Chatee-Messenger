using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class UserListItemDesignModel : UserListItemViewModel
    {
        #region Singleton
        public static UserListItemDesignModel Instance => new UserListItemDesignModel();
        #endregion
        #region Constructor
        public UserListItemDesignModel()
        {
            // TODO: Get rid of commented initialisations in design model
            //Initials = "VK";
            //Name = "Vladyslav";
            //ProfilePictureRGB = "ffdf08";
        }
        #endregion
    }
}
