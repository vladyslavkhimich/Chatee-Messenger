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
            User interlocutor = new User(User);
            if (ApplicationViewModel.ClientDatabase.ChatContracts.ToList().Find(chat => ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID == chat.UserID1 && interlocutor.UserID == chat.UserID2 || interlocutor.UserID == chat.UserID2 && ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID == chat.UserID1) == null)
            { 
                bool isServerHasChat = ApplicationViewModel.ServiceClient.IsServerHasChat(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, interlocutor.UserID);
                if (!isServerHasChat)
                {
                    ChatContract newChat = CreateLocalChat(interlocutor);
                    ChatMessageListViewModel TestMessageList = new ChatMessageListViewModel(interlocutor, newChat);
                    IoCContainer.Get<ApplicationViewModel>().UserChatMessageLists.Add(TestMessageList);
                    foreach (var chatListItem in IoCContainer.Get<ChatListViewModel>().Chats)
                        chatListItem.IsSelected = false;
                    IoCContainer.Get<ChatListViewModel>().Chats.ToList().Find(chatListItem => chatListItem.Chat.ServerDatabaseChatID == TestMessageList.Chat.ServerDatabaseChatID).IsSelected = true;
                    IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, TestMessageList);
                }
                else
                {
                    ChatMessageListViewModel chatToGoTo = IoCContainer.Get<ApplicationViewModel>().UserChatMessageLists.ToList().Find(chatMessageList => chatMessageList.Interlocutor.UserID == interlocutor.UserID);
                    IoCContainer.Get<ChatListViewModel>().Chats.ToList().Find(chatListItem => chatListItem.Chat.ServerDatabaseChatID == chatToGoTo.Chat.ServerDatabaseChatID).IsSelected = true;
                    IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, chatToGoTo);
                }
            }
            
        }
        #endregion
        #region Helper Methods
        public ChatContract CreateLocalChat(User interlocutor)
        {
            ChatContract newChat = new ChatContract(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, interlocutor.UserID, ApplicationViewModel.ServiceClient.GetNextChatID(), new ObservableCollection<MessageContract>());
            ApplicationViewModel.UserInterlocutors.Add(new UserContract(interlocutor.UserID, interlocutor.Username, interlocutor.Name, interlocutor.Bio, interlocutor.Initials, interlocutor.ProfilePictureRGB));
            IoCContainer.Get<UserListViewModel>().SetUsersFromDatabase(ApplicationViewModel.UserInterlocutors.ToList());
            IoCContainer.Get<ChatListViewModel>().Chats.Add(new ChatListItemViewModel(interlocutor, newChat));
            return newChat;
        }
        #endregion
    }
}
