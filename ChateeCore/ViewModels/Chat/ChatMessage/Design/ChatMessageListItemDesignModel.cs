using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatMessageListItemDesignModel : ChatMessageListItemViewModel
    {
        #region Singleton
        public static ChatMessageListItemDesignModel Instance => new ChatMessageListItemDesignModel();
        #endregion
        #region Constructor
        public ChatMessageListItemDesignModel()
        {
            User.Initials = "OT";
            User.LastMessage = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            User.ProfilePictureRGB = "ffdf08";
            Message.IsSentByMe = true;
            Message.MessageSentTime = DateTime.UtcNow;
            Message.MessageReadTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1.1));
        }
        #endregion
    }
}
