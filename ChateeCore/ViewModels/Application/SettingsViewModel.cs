using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties
        public User User => IoCContainer.Get<ApplicationViewModel>().CurrentUser;
        public TextEntryViewModel Name { get; set; }
        public TextEntryViewModel Username { get; set; }
        public TextEntryViewModel Bio { get; set; }
        public PasswordEntryViewModel Password { get; set; }
        public TextEntryViewModel Email { get; set; }
        #endregion
        #region Public Commands
        public ICommand CloseCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ClearUserDataCommand { get; set; }
        public ICommand EditImageCommand { get; set; }
        #endregion
        #region Constructors
        public SettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            OpenCommand = new RelayCommand(Open);
            LogoutCommand = new RelayCommand(Logout);
            ClearUserDataCommand = new RelayCommand(ClearUserData);
            EditImageCommand = new RelayCommand(EditImage);
        }
        #endregion
        #region Commands Methods
        public void Close()
        {
            IoCContainer.Get<ApplicationViewModel>().IsSettingsMenuVisible = false;
        }
        public void Open()
        {
            IoCContainer.Get<ApplicationViewModel>().IsSettingsMenuVisible = true;
        }
        public void Logout()
        {
            ClearUserData();
            IoCContainer.Get<ApplicationViewModel>().IsSideMenuVisible = false;
            IoCContainer.Get<ApplicationViewModel>().IsSettingsMenuVisible = false;
            IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.LoginPage);
        }
        public void ClearUserData()
        {
            Name = null;
            Username = null;
            Password = null;
            Email = null;
        }
        public void SetUsersData()
        {
            Name = new TextEntryViewModel { Label = "Name", OriginalText = User.Name };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = User.Username };
            Bio = new TextEntryViewModel { Label = "Bio", OriginalText = User.Bio };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = User.Email };
        }
        public void EditImage()
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.ShowDialog();
        }
        #endregion
    }
}
