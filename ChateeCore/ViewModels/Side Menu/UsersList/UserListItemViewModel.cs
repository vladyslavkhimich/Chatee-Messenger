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
        public bool IsHasChat { get; set; }
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
            if (user.Chats.ToList().Find(chat => chat.UserID1 == IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.ServerDatabaseUserID || chat.UserID2 == IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.ServerDatabaseUserID) != null)
                IsHasChat = true;
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
