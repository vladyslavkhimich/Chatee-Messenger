using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class LoginPageViewModel : BaseViewModel
    {
        // TODO: Make a password renewal function
        // TODO: Make a custom MessageBox
        #region Public Properties
        public int SelectedTabIndex { get; set; }
        public string RegisterUsername { get; set; }
        public string RegisterName { get; set; }
        public string RegisterEmail { get; set; }
        public string LoginEmail { get; set; }
        public string LoginPasswordErrorToolTip { get; set; }
        public string RegisterPasswordErrorToolTip { get; set; }
        public string RepeatRegisterPasswordErrorToolTip { get; set; }
        public SecureString LoginPassword { get; set; }
        public SecureString RegisterPassword { get; set; }
        public SecureString RepeatRegisterPassword { get; set; }
        public bool IsKeepLoggedIn { get; 
            set; }
        public bool IsRegisterUsernameHasError { get; set; }
        public bool IsRegisterNameHasError { get; set; }
        public bool IsLoginEmailHasError { get; set; }
        public bool IsRegisterEmailHasError { get; set; }
        public bool IsLoginPasswordHasError { get; set; }
        public bool IsRegisterPasswordHasError { get; set; }
        public bool IsRepeatRegisterPasswordHasError { get; set; }
        #endregion
        #region Commands
        public ICommand SwitchToChatPageCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ApplicationViewModel ApplicationViewModel { get; set; } = IoCContainer.Get<ApplicationViewModel>();
        #endregion
        #region Constructors
        public LoginPageViewModel()
        {
            SetLoginPageCommands();
            
        }
        #endregion
        #region Commands Methods
        public void Register()
        {
            
            IsRegisterPasswordHasError = false;
            IsRepeatRegisterPasswordHasError = false;
            if(!ApplicationViewModel.IsServerReachable)
            {
                MessageBox.Show("Check your internet connection", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (SecureStringHelpers.Unsecure(RegisterPassword) != SecureStringHelpers.Unsecure(RepeatRegisterPassword))
            {
                IsRegisterPasswordHasError = true;
                IsRepeatRegisterPasswordHasError = true;
                RegisterPasswordErrorToolTip = "Passwords are not equal";
                RepeatRegisterPasswordErrorToolTip = "Passwords are not equal";
            }
            if (IsRegisterEmailHasError || IsRegisterUsernameHasError || IsRegisterNameHasError || IsRegisterPasswordHasError || IsRepeatRegisterPasswordHasError || string.IsNullOrEmpty(RegisterUsername) || string.IsNullOrEmpty(RegisterName) || string.IsNullOrEmpty(RegisterEmail) || string.IsNullOrEmpty(SecureStringHelpers.Unsecure(RegisterPassword)) || string.IsNullOrEmpty(SecureStringHelpers.Unsecure(RepeatRegisterPassword)))
            {
                MessageBox.Show("Enter valid values", "Registration error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool IsRegisterDone = ApplicationViewModel.ServiceClient.Register(new WCF_Server.DataContracts.UserContract
            {
                Username = RegisterUsername,
                Name = RegisterName,
                Email = RegisterEmail,
                PasswordHash = SecureStringHelpers.SecureStringToHash(RegisterPassword, ApplicationViewModel.ServiceClient.GetNextUserID()),
                AvatarBytes = FileHelper.ConvertFileToArrayOfBytes(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)  + "/ClientDatabase/Database/ImageDatabase/Avatars/black-tea.png"),
                AvatarCheckSum = FileHelper.ComputeFileCheckSum(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/ImageDatabase/Avatars/black-tea.png"),
                AvatarFileName = "black-tea.png",
                Bio = string.Empty,
                Initials = SetInitialsFromName(RegisterName),
                ProfilePictureRGB = GenerateProfilePictureRGB()
            });
            if (IsRegisterDone)
            {
                SelectedTabIndex = 0;
                ClearRegisterTextBoxes();
            }
        }
        public void Login()
        {
            if (IsLoginEmailHasError || IsLoginPasswordHasError)
            {
                MessageBox.Show("Enter valid values", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var loggedUserClientDatabase = ApplicationViewModel.ClientDatabase.UserContracts.ToList().Find(user => user.Email == LoginEmail);               
            if (loggedUserClientDatabase == null && ApplicationViewModel.IsServerReachable)
            {
                var loggedUser = ApplicationViewModel.ServiceClient.GetUserByEmail(LoginEmail);
                if (loggedUser == null)
                {
                    MessageBox.Show("There is no user with such email", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!loggedUser.PasswordHash.SequenceEqual(SecureStringHelpers.SecureStringToHash(LoginPassword, loggedUser.UserID)))
                {
                    MessageBox.Show("Incorrect e-mail or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                LoginHelper.ChangeClientDatabaseUser(loggedUser, IsKeepLoggedIn);
                loggedUserClientDatabase = ApplicationViewModel.ClientDatabase.UserContracts.ToList().Last();
                LoginHelper.SetApplicationUser(loggedUserClientDatabase, loggedUser.UserID);
                LoginHelper.SetUsersAndFilesLists();
                SwitchToChatPage();
            }
            else if (loggedUserClientDatabase != null && !ApplicationViewModel.IsServerReachable)
            {
                if (!loggedUserClientDatabase.PasswordHash.SequenceEqual(SecureStringHelpers.SecureStringToHash(LoginPassword, loggedUserClientDatabase.ServerDatabaseUserID)))
                {
                    MessageBox.Show("Incorrect e-mail or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (loggedUserClientDatabase.IsKeepLoggedIn != IsKeepLoggedIn)
                    LoginHelper.ChangeClientDatabaseUser(loggedUserClientDatabase, IsKeepLoggedIn);
                LoginHelper.SetApplicationUser(loggedUserClientDatabase, loggedUserClientDatabase.ServerDatabaseUserID);
                SwitchToChatPage();
            }
            else if (loggedUserClientDatabase != null && ApplicationViewModel.IsServerReachable)
            {
                if (!loggedUserClientDatabase.PasswordHash.SequenceEqual(SecureStringHelpers.SecureStringToHash(LoginPassword, loggedUserClientDatabase.ServerDatabaseUserID)))
                {
                    MessageBox.Show("Incorrect e-mail or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var loggedUser = ApplicationViewModel.ServiceClient.GetUserByEmail(LoginEmail);
                if (!LoginHelper.IsServerAndClientUserSame(loggedUserClientDatabase, loggedUser))
                    LoginHelper.ChangeClientDatabaseUser(loggedUser, IsKeepLoggedIn);
                if (loggedUserClientDatabase.IsKeepLoggedIn != IsKeepLoggedIn)
                    LoginHelper.ChangeClientDatabaseUser(loggedUser, IsKeepLoggedIn);
                if(!string.Equals(loggedUser.AvatarCheckSum, loggedUserClientDatabase.AvatarCheckSum))
                {
                    if (!LoginHelper.IsClientDatabaseHasAvatar(loggedUser.AvatarCheckSum))
                        Task.Run(() => LoginHelper.SetUserAvatar(loggedUser.UserID, loggedUser.AvatarCheckSum, loggedUser.AvatarFileName, loggedUser.AvatarBytes));
                    else
                    {
                        Task<string> searchingAvatarTask = Task.Run(() => FileHelper.GetFilePathWithCheckSum(loggedUser.AvatarCheckSum, "/ClientDatabase/Database/ImageDatabase/Avatars/"));
                        string avatarPath = searchingAvatarTask.Result;
                        Task.Run(() => LoginHelper.SetUserAvatar(loggedUser.UserID, FileHelper.ComputeFileCheckSum(avatarPath), new FileInfo(avatarPath).Name));
                    }
                }
                loggedUserClientDatabase = ApplicationViewModel.ClientDatabase.UserContracts.ToList().Last();
                LoginHelper.SetApplicationUser(loggedUserClientDatabase, loggedUserClientDatabase.ServerDatabaseUserID);
                LoginHelper.SetUsersAndFilesLists();
                SwitchToChatPage();
            }
            else if (loggedUserClientDatabase == null && !ApplicationViewModel.IsServerReachable)
                MessageBox.Show("Check your network connection!", "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void SwitchToChatPage()
        {
            IoCContainer.Get<SettingsViewModel>().SetUsersData();
            ApplicationViewModel.GoToPage(ApplicationPages.ChatPage);
            ApplicationViewModel.IsSideMenuVisible = true;
        }
        #endregion
        #region Helper Methods
        public void SetLoginPageCommands()
        {
            SwitchToChatPageCommand = new RelayCommand(SwitchToChatPage);
            RegisterCommand = new RelayCommand(Register);
            LoginCommand = new RelayCommand(Login);
        }
        
        
        public string SetInitialsFromName(string name)
        {
            string initials = string.Empty;
            name = Regex.Replace(name, @"\s+", " ");
            string[] nameWords = name.Split(' ');
            if (nameWords.Count() == 1)
            {
                initials += nameWords[0].ToCharArray()[0];
                initials += nameWords[0].ToCharArray()[0];
            }
            else
            {
                for (int i = 0; i < 2; i++)
                    initials += nameWords[i].ToCharArray()[0];
            }
            return initials.ToUpper();
        }
        public string GenerateProfilePictureRGB()
        {
            string profilePictureRGB = string.Empty;
            Random byteGenerator = new Random();
            for (int i = 0; i < 3; i++)
            {
                int byteValue = byteGenerator.Next(17,255);
                profilePictureRGB += byteValue.ToString("X");
            }
            return profilePictureRGB.ToLower();
        }
        public void ClearRegisterTextBoxes()
        {
            RegisterUsername = string.Empty;
            RegisterName = string.Empty;
            RegisterEmail = string.Empty;
            RegisterPassword = null;
            RepeatRegisterPassword = null;
        }
        #endregion
    }
}
