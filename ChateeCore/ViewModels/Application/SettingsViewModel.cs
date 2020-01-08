using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties
        public ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
        public User User => IoCContainer.Get<ApplicationViewModel>().CurrentUser;
        public TextEntryViewModel Name { get; set; }
        public TextEntryViewModel Username { get; set; }
        public TextEntryViewModel Bio { get; set; }
        public PasswordEntryViewModel Password { get; set; }
        public TextEntryViewModel Email { get; set; }
        public string UserProfileImagePath { get; set; }
        #endregion
        #region Public Commands
        public ICommand CloseCommand { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand ClearUserDataCommand { get; set; }
        public ICommand EditImageCommand { get; set; }
        public ICommand SaveNameCommand { get; set; }
        public ICommand SaveUsernameCommand { get; set; }
        public ICommand SaveBioCommand { get; set; }
        public ICommand SaveEmailCommand { get; set; }
        #endregion
        #region Constructors
        public SettingsViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            OpenCommand = new RelayCommand(Open);
            LogoutCommand = new RelayCommand(Logout);
            ClearUserDataCommand = new RelayCommand(ClearUserData);
            EditImageCommand = new RelayCommand(EditImage);
            SaveNameCommand = new RelayCommand(async () => await SaveNameAsync());
            SaveUsernameCommand = new RelayCommand(async () => await SaveUsernameAsync());
            SaveBioCommand = new RelayCommand(async () => await SaveBioAsync());
            SaveEmailCommand = new RelayCommand(async () => await SaveEmailAsync());
        }
        #endregion
        #region Commands Methods
        public void Close()
        {
            ApplicationViewModel.IsSettingsMenuVisible = false;
        }
        public void Open()
        {
            ApplicationViewModel.IsSettingsMenuVisible = true;
        }
        public void Logout()
        {
            ApplicationViewModel.ServiceClient.Disconnect(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID);
            LoginHelper.RemoveClientDatabaseUser(ApplicationViewModel.CurrentUserContract);
            ClearUserData();
            ApplicationViewModel.IsSideMenuVisible = false;
            ApplicationViewModel.IsSettingsMenuVisible = false;            
            ApplicationViewModel.GoToPage(ApplicationPages.LoginPage);
        }
        public void ClearUserData()
        {
            ApplicationViewModel.CurrentUser = null;
            ApplicationViewModel.CurrentUserContract = null;
            ApplicationViewModel.UserChatMessageLists = new ObservableCollection<ChatMessageListViewModel>();
            IoCContainer.Get<FileListViewModel>().Files = new ObservableCollection<FileListItemViewModel>();
        }
        public void SetUsersData()
        {
            Name = new TextEntryViewModel
            {
                Label = "Name",
                OriginalText = User.Name,
                CommitAction = SaveNameAsync
                
            };
            Username = new TextEntryViewModel 
            { 
                Label = "Username", 
                OriginalText = User.Username,
                CommitAction = SaveUsernameAsync
            };
            Bio = new TextEntryViewModel 
            { 
                Label = "Bio", 
                OriginalText = User.Bio,
                CommitAction = SaveBioAsync
            };
            Password = new PasswordEntryViewModel 
            { 
                Label = "Password", 
                FakePassword = "********",
                CommitAction = SavePasswordAsync
            };
            Email = new TextEntryViewModel 
            { 
                Label = "Email", 
                OriginalText = User.Email,
                CommitAction = SaveEmailAsync
            };
        }
        public void EditImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files(*.jpg *.jpeg *.png)|*.jpg;*.jpeg;*.png"
            };
            if((bool)openFileDialog.ShowDialog())
            {
                LoginHelper.SetUserAvatar(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, FileHelper.ComputeFileCheckSum(openFileDialog.FileName), new FileInfo(openFileDialog.FileName).Name, FileHelper.ConvertFileToArrayOfBytes(openFileDialog.FileName));
            }
        }
        #endregion
        #region Changing User Settings Methods
        public async Task<bool> SaveNameAsync()
        {
            await Task.Run(() => ApplicationViewModel.ServiceClient.ChangeUserName(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Name.EditedText));
            return true;
        }
        public async Task<bool> SaveUsernameAsync()
        {
            await Task.Run(() => ApplicationViewModel.ServiceClient.ChangeUserUsername(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Username.EditedText));
            return true;
        }
        public async Task<bool> SaveBioAsync()
        {
            await Task.Run(() => ApplicationViewModel.ServiceClient.ChangeUserBio(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Bio.EditedText));
            return true;
        }
        public async Task<bool> SaveEmailAsync()
        {
            await Task.Run(() => ApplicationViewModel.ServiceClient.ChangeUserEmail(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Email.EditedText));
            return true;
        }
        public async Task<bool> SavePasswordAsync()
        {
            await Task.Run(() => ApplicationViewModel.ServiceClient.ChangeUserPassword(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, SecureStringHelpers.SecureStringToHash(Password.NewPassword, ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID)));
            return true;
        }
        #endregion
    }
}
