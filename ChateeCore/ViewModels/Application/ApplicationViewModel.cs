using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ChateeCore.ServiceReference;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class ApplicationViewModel : BaseViewModel, IServiceCallback
    {
        #region Public Properties
        public ServiceClient ServiceClient { get; set; }
        public ClientDatabase ClientDatabase { get; set; }
        public UserContract CurrentUserContract { get; set; }
        public User CurrentUser { get; set; }
        public ObservableCollection<UserContract> UserInterlocutors { get; set; } = new ObservableCollection<UserContract>();
        public ApplicationPages CurrentPage { get; private set; } = ApplicationPages.LoginPage;
        public SideMenuControls CurrentControl { get; private set; } = SideMenuControls.ChatList;
        public BaseViewModel CurrentPageViewModel { get; set; }
        public bool IsSideMenuVisible { get; set; } = false;
        public bool IsSettingsMenuVisible { get; set; } = false;
        public bool IsUserInformationVisible { get; set; } = false;
        public bool IsUserLogged => CurrentUser != null;
        public bool IsServerReachable { get; set; } = true;
        #endregion
        #region Public Commands
        public ICommand OpenChatListCommand { get; set; } 
        public ICommand OpenUserListCommand { get; set; } 
        public ICommand OpenFileListCommand { get; set; } 
        #endregion
        #region Constructors
        public ApplicationViewModel()
        {
            OpenChatListCommand = new RelayCommand(OpenChatList);
            OpenUserListCommand = new RelayCommand(OpenUserList);
            OpenFileListCommand = new RelayCommand(OpenFileList);
            ClientDatabase = new ClientDatabase();
            ServiceClient = new ServiceClient(new InstanceContext(this));
            //CheckConnectionAsync();
        }
        #endregion
        #region Helper Methods
        public void GoToPage(ApplicationPages page, BaseViewModel viewModel = null)
        {
            IsUserInformationVisible = false;
            IsSettingsMenuVisible = false;
            CurrentPageViewModel = viewModel;

            CurrentPage = page;
            OnPropertyChanged(nameof(CurrentPage));
            IsSideMenuVisible = page == ApplicationPages.ChatPage;
        }
        public void CheckConnectionAsync()
        {
            CheckConnection();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);
            System.Timers.Timer timer = new System.Timers.Timer(TimeSpan.FromSeconds(5).TotalMilliseconds);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await Task.Run(() => CheckConnection());
        }

        public void CheckConnection()
        {
            try
            {
                ServiceClient.Connect();
                IsServerReachable = true;
                if (IsUserLogged)
                    ServiceClient.LoginOnServer(CurrentUserContract);
            }
            catch
            {
                IsServerReachable = false;
                ServiceClient = new ServiceClient(new InstanceContext(this));
            }
        }
        #endregion
        #region Commands Methods
        public void OpenChatList()
        {
            CurrentControl = SideMenuControls.ChatList;
        }
        public void OpenUserList()
        {
            CurrentControl = SideMenuControls.UserList;
        }
        public void OpenFileList()
        {
            CurrentControl = SideMenuControls.FileList;
        }

        public void ConnectCallback(string testCallbackString)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
