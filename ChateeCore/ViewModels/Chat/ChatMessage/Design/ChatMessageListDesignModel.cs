using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        #region Singleton
        public static ChatMessageListDesignModel Instance => new ChatMessageListDesignModel();
        #endregion
        #region Constructor
        public ChatMessageListDesignModel()
        {
            Chats = new List<ChatMessageListItemViewModel>
            {
                new ChatMessageListItemViewModel
                {
                    SenderName = "Oleg",
                    Initials = "OT",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "f5dff8",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    IsSentByMe = false
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Vladyslav",
                    Initials = "VK",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1.1)),
                    IsSentByMe = true
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Oleg",
                    Initials = "OT",
                    Message = "Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "f5dff8",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    IsSentByMe = false
                }
            };
        }
        #endregion
    }
}
