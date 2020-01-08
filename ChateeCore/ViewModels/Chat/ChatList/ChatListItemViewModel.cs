using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class ChatListItemViewModel : BaseViewModel
    {
        #region Public Properties
        public User Interlocutor { get; set; }
        public ChatContract Chat { get; set; }
        public bool IsSelected { get; set; }
        public bool IsNewMessageAvailable
        {
            get
            {
                return Chat.IsHasNewMessages;
            }
            set
            {
                Chat.IsHasNewMessages = value;
            }
        }
        public string LastMessage { get; set; }
        public ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
        #endregion
        #region Public Commands
        public ICommand OpenMessageCommand { get; set; }
        #endregion
        #region Constructors
        public ChatListItemViewModel()
        {
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }
        public ChatListItemViewModel(User interlocutor, ChatContract chat)
        {
            Interlocutor = interlocutor;
            Chat = chat;
            if(Chat.Messages.Count != 0)
            LastMessage = Chat.Messages.Last().MessageText;
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }
        #endregion
        #region Commands Methods
        public void OpenMessage()
        {
            IsNewMessageAvailable = false;
            foreach (var chatListItem in IoCContainer.Get<ChatListViewModel>().Chats)
                chatListItem.IsSelected = false;
            IsSelected = true;
            ChatMessageListViewModel chatToOpen = ApplicationViewModel.UserChatMessageLists.ToList().Find(chatMessageList => chatMessageList.Chat.ChatID == Chat.ChatID);
            List<MessageContract> notReadMessageContracts = chatToOpen.Chat.Messages.ToList().FindAll(message => message.MessageReadTime == new DateTime(2000, 1, 1));
            List<ChatMessageListItemViewModel> notReadMessageListItems = chatToOpen.Messages.ToList().FindAll(message => message.Message.MessageReadTime == new DateTime(2000, 1, 1));
            List<Message> notReadMessages = new List<Message>();
            notReadMessageListItems.ToList().ForEach(notReadMessageListItem => notReadMessages.Add(notReadMessageListItem.Message));
            for(int i = 0; i < notReadMessages.Count; i++)
            {
                if (notReadMessages[i].UserID == ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID)
                    continue;
                notReadMessages[i].MessageReadTime = DateTime.UtcNow;
                MessageContract clientDatabaseMessage = ApplicationViewModel.ClientDatabase.MessageContracts.ToList().Find(messageContract => messageContract.ServerDatabaseMessageID == notReadMessageContracts[i].MessageID);
                clientDatabaseMessage.MessageReadTime = DateTime.UtcNow;
                ApplicationViewModel.ServiceClient.SetMessageReadTime(notReadMessages[i].MessageID, DateTime.UtcNow);
                ApplicationViewModel.ClientDatabase.Entry(clientDatabaseMessage).State = EntityState.Modified;
                ApplicationViewModel.ClientDatabase.SaveChanges();
            }
            ApplicationViewModel.GoToPage(ApplicationPages.ChatPage, chatToOpen);
        }
        #endregion
    }
}
