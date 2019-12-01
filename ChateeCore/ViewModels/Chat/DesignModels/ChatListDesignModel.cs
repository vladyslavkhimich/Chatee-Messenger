using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatListDesignModel : ChatListViewModel
    {
        #region Singleton
        public static ChatListDesignModel Instance => new ChatListDesignModel();
        #endregion
        #region Constructor
        public ChatListDesignModel()
        {
            Chats = new List<ChatListItemViewModel>
            {
                new ChatListItemViewModel
                {
                    Initials = "VK",
                    Name = "Vladyslav",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08"
                },
                new ChatListItemViewModel
                {
                    Initials = "OT",
                    Name = "Oleg",
                    Message = "Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "f5dff8",
                    NewMessageAvailable = true,
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Initials = "MS",
                    Name = "Misha",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "78d10d"
                },
                new ChatListItemViewModel
                {
                    Initials = "VK",
                    Name = "Vladyslav",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08"
                },
                new ChatListItemViewModel
                {
                    Initials = "VK",
                    Name = "Vladyslav",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08"
                },
                new ChatListItemViewModel
                {
                    Initials = "VK",
                    Name = "Vladyslav",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08"
                },
                new ChatListItemViewModel
                {
                    Initials = "VK",
                    Name = "Vladyslav",
                    Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ProfilePictureRGB = "ffdf08"
                }
            };
        }
        #endregion
    }
}
