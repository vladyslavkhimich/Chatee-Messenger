using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Commands
        public RelayCommand SwitchToChatPageCommand { get; set; }
        public ApplicationViewModel ApplicationViewModel { get; set; } = IoCContainer.Get<ApplicationViewModel>();
        #endregion
        #region Constructors
        public LoginPageViewModel()
        {
            SetLoginPageComamnds();
        }
        #endregion
        #region Helper Methods
        public void SetLoginPageComamnds()
        {
            SwitchToChatPageCommand = new RelayCommand(switchToChatPageMethod);

        }
        #endregion
        #region Commands Methods
        public void switchToChatPageMethod()
        {
            ApplicationViewModel.CurrentPage = ApplicationPages.ChatPage;
            ApplicationViewModel.IsSideMenuVisible = true;
        }
        #endregion
    }
}
