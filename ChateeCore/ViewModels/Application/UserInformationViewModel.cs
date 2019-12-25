using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class UserInformationViewModel : BaseViewModel
    {
        #region Public Properties
        public User User { get; set; }
        public bool IsOpenChatVisible { get; set; } = false;
        #endregion
        #region Public Commands
        public ICommand CloseCommand { get; set; }
        public ICommand OpenChatCommand { get; set; }
        #endregion
        #region Constructors
        public UserInformationViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            OpenChatCommand = new RelayCommand(OpenChat);

            //NameDisplayer = new TextDisplayerViewModel
            //{
            //    Label = "Name",
            //    Text = "DefaultText"
            //};
            //BioDisplayer = new TextDisplayerViewModel
            //{
            //    Label = "Bio",
            //    Text = "DefaultText"
            //};
        }
        public UserInformationViewModel(User user, bool isOpenChatVisible)
        {
            CloseCommand = new RelayCommand(Close);
            User = user;
            IsOpenChatVisible = isOpenChatVisible;
        }
        #endregion
        #region Commands Methods
        public void Close()
        {
            IoCContainer.Get<ApplicationViewModel>().IsUserInformationVisible = false;
        }
        public void OpenChat()
        {
            User TestUser = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
            ObservableCollection<Message> TestMessages = new ObservableCollection<Message>();
            TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "C:/Users/Владелец/source/repos/Chatee/ChateeWPF/Images/Samples/black-tea.png"));
            TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "D:/Test/TestFiles/Palermo Story.txt"));

            Chat TestChat = new Chat(1, 1, 2, TestMessages);
            ChatMessageListViewModel TestMessageList = new ChatMessageListViewModel(TestUser, TestChat);
            IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, TestMessageList);
        }
        #endregion
    }
}
