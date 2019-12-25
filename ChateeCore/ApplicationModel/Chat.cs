using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class Chat
    {
        #region Public Properties
        public int ChatID { get; set; }
        public int UserID1 { get; set; }
        public int UserID2 { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        public ObservableCollection<FileAttachment> Files { get; set; }
        #endregion
        #region Constructors
        public Chat()
        {

        }
        public Chat(int chatID, int userID1, int userID2, ObservableCollection<Message> messages)
        {
            ChatID = chatID;
            UserID1 = userID1;
            UserID2 = userID2;
            Messages = new ObservableCollection<Message>(messages);
        }
        public Chat(Chat chat)
        {
            ChatID = chat.ChatID;
            UserID1 = chat.UserID1;
            UserID2 = chat.UserID2;
            Messages = new ObservableCollection<Message>(chat.Messages);
            Files = new ObservableCollection<FileAttachment>(chat.Files);
        }
        public Chat(WCF_Server.DataContracts.ChatContract chatContract)
        {
            ChatID = chatContract.ChatID;
            UserID1 = chatContract.UserID1;
            UserID2 = chatContract.UserID2;
            Messages = new ObservableCollection<Message>();
            if(chatContract.Messages != null)
              foreach (var chatMessage in chatContract.Messages)
                  Messages.Add(new Message(chatMessage));
        }
        #endregion
    }
}
