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
            Initials = "VK";
            SenderName = "Vladyslav";
            Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
            ProfilePictureRGB = "ffdf08";
            IsSentByMe = true;
            MessageSentTime = DateTimeOffset.UtcNow;
            MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.1));
        }
        #endregion
    }
}
