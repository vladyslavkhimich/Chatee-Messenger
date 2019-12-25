using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class UserListItemViewModel : BaseViewModel
    {
        #region Public Properties
        public User User { get; set; }
        // TODO: Define if users have chat
        // TODO: Uncomment line of defining if users have chat
        public bool IsHasChat => (bool)User.Chats?.Intersect(IoCContainer.Get<ApplicationViewModel>().CurrentUser.Chats).Any();
        //public bool IsHasChat => false;
        #endregion
        #region Public Commands
        public ICommand OpenUserProfileCommand { get; set; }
        #endregion
        #region Constructors
        public UserListItemViewModel()
        {
            OpenUserProfileCommand = new RelayCommand(OpenUserProfile);
        }
        public UserListItemViewModel(User user)
        {
            User = user;
            OpenUserProfileCommand = new RelayCommand(OpenUserProfile);
        }
        #endregion
        #region Commands Methods
        public void OpenUserProfile()
        {
            IoCContainer.Get<UserInformationViewModel>().User = User;
            IoCContainer.Get<UserInformationViewModel>().IsOpenChatVisible = true;
            IoCContainer.Get<ApplicationViewModel>().IsUserInformationVisible = true;
        }
        #endregion
    }
}
