using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            // TODO: Get rid of commented initialisations in design model
            //ObservableCollection<ChatListItemViewModel> TestListItems = new ObservableCollection<ChatListItemViewModel>();
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Miha", "mihanya228@gmail.com", "Misha", "Wordpress is power", "MS", "EF1266", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Violent", "himich01092001@gmail.com", "Vladyslav", "Fuck da maximalism", "VK", "889F66", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."), 1));
            //TestListItems.Add(new ChatListItemViewModel(new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto."), 1));
            //Chats = new ObservableCollection<ChatListItemViewModel>(TestListItems);
            //Chats = new ObservableCollection<ChatListItemViewModel>
            //{
            //    new ChatListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Violent",
            //        Bio = "Fuck da maximalism.",
            //        Name = "Vladyslav",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "OT",
            //        Name = "Oleg",
            //        Message = "Consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "f5dff8",
            //        NewMessageAvailable = true,
            //        IsSelected = true
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "MS",
            //        Name = "Misha",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "78d10d"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "VK",
            //        Name = "Vladyslav",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "VK",
            //        Name = "Vladyslav",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "VK",
            //        Name = "Vladyslav",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new ChatListItemViewModel
            //    {
            //        Initials = "VK",
            //        Name = "Vladyslav",
            //        Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
            //        ProfilePictureRGB = "ffdf08"
            //    }
            //};
        }
        #endregion
    }
}
