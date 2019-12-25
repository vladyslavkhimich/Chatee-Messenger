using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class UserInformationDesignModel : UserInformationViewModel
    {
        #region Singleton
        public static UserInformationDesignModel Instance => new UserInformationDesignModel();
        #endregion
        #region Constructors
        public UserInformationDesignModel()
        {
            User = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
        }
        #endregion
    }
}
