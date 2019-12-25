using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class UserListDesignModel : UserListViewModel
    {
        #region Singleton
        public static UserListDesignModel Instance => new UserListDesignModel();
        #endregion
        #region Constructor
        public UserListDesignModel()
        {
            // TODO: Get rid of commented initialisations in design model
            //Users = new ObservableCollection<UserListItemViewModel>
            //{
            //    new UserListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Violent",
            //        Bio = "Fuck da maximalism.",
            //        Name = "Vladyslav",
            //        ProfilePictureRGB = "ffdf08",
            //        IsHasChat = true,
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "OT",
            //        Username = "Vidzhel",
            //        Bio = "Fond your direction through introspection.",
            //        Name = "Oleg",
            //        ProfilePictureRGB = "f5dff8",
            //        IsHasChat = true
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "MS",
            //        Name = "Misha",
            //        Username = "Miha",
            //        Bio = "Wordpress is power.",
            //        ProfilePictureRGB = "78d10d",
            //        IsHasChat = true
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Miha",
            //        Name = "Vladyslav",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Miha",
            //        Name = "Vladyslav",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Miha",
            //        Name = "Vladyslav",
            //        ProfilePictureRGB = "ffdf08"
            //    },
            //    new UserListItemViewModel
            //    {
            //        Initials = "VK",
            //        Username = "Miha",
            //        Name = "Vladyslav",
            //        ProfilePictureRGB = "ffdf08"
            //    }
            //};
        }
        #endregion
    }
}
