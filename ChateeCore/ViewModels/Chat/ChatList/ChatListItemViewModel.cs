using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        // TODO: Notify if new message available
        #region Public Properties
        public User Interlocutor { get; set; }
        public ChatContract Chat { get; set; }
        public bool IsSelected { get; set; }
        public string LastMessage { get; set; }
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
            LastMessage = Chat.Messages.Last().MessageText;
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }
        #endregion
        #region Commands Methods
        public void OpenMessage()
        {
            Chat.IsHasNewMessages = false;
            IsSelected = true;
            IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, IoCContainer.Get<ApplicationViewModel>().UserChatMessageLists.ToList().Find(chatMessageList => chatMessageList.Chat.ChatID == Chat.ChatID));
        }
        #endregion
    }
}
