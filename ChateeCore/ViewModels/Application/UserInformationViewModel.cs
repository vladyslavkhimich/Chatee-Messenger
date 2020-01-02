using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class UserInformationViewModel : BaseViewModel
    {
        #region Public Properties
        public User User { get; set; }
        public bool IsOpenChatVisible { get; set; } = false;
        public ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
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
            User Interlocutor = new User(User);
            if (ApplicationViewModel.ClientDatabase.ChatContracts.ToList().Find(chat => ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID == chat.UserID1 && Interlocutor.UserID == chat.UserID2 || Interlocutor.UserID == chat.UserID2 && ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID == chat.UserID1) == null)
            { 
                bool isServerHasChat = ApplicationViewModel.ServiceClient.IsServerHasChat(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Interlocutor.UserID);
                if (!isServerHasChat)
                {
                    Chat newChat = CreateLocalChat(Interlocutor);
                    ChatMessageListViewModel TestMessageList = new ChatMessageListViewModel(Interlocutor, newChat);
                    IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, TestMessageList);
                }
            }
            //ObservableCollection<Message> TestMessages = new ObservableCollection<Message>();
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "C:/Users/Владелец/source/repos/Chatee/ChateeWPF/Images/Samples/black-tea.png"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "D:/Test/TestFiles/Palermo Story.txt"));

            //Chat TestChat = new Chat(1, 1, 2, TestMessages);
            
        }
        #endregion
        #region Helper Methods
        public Chat CreateLocalChat(User interlocutor)
        {
            Chat newChat = new Chat(ApplicationViewModel.ServiceClient.GetNextChatID(), ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, interlocutor.UserID);
            IoCContainer.Get<ChatListViewModel>().Chats.Add(new ChatListItemViewModel(interlocutor, newChat));
            return newChat;
        }
        #endregion
    }
}
