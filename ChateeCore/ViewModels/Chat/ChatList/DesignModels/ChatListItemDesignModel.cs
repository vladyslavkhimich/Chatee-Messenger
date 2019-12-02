using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        #region Singleton
        public static ChatListItemDesignModel Instance => new ChatListItemDesignModel();
        #endregion
        #region Constructor
        public ChatListItemDesignModel()
        {
            Initials = "VK";
            Name = "Vladyslav";
            Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            ProfilePictureRGB = "ffdf08";
        }
        #endregion
    }
}
