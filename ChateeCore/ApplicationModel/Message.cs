using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class Message
    {
        #region Public Properties
        public int ChatID { get; set; }
        public int MessageID { get; set; }
        public int UserID { get; set; }
        public string MessageText { get; set; }
        public bool IsSentByMe => UserID == IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.ServerDatabaseUserID;
        public bool IsHasFileAttachment { get; set; }
        public bool IsHasImageAttachment { get; set; }
        public bool IsMessageRead => MessageReadTime > new DateTime(2000, 1, 1);
        public DateTime MessageReadTime { get; set; }
        public DateTime MessageSentTime { get; set; }
        public string FileCheckSum { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        #endregion
        #region Constructors
        public Message()
        {

        }
        public Message(int chatID, int messageID, int userID, DateTime messageReadTime, DateTime messageSentTime, string messageText = "", string fileCheckSum = "", string filePath = "")
        {
            ChatID = chatID;
            MessageID = messageID;
            UserID = userID;
            MessageText = messageText;
            MessageReadTime = messageReadTime;
            MessageSentTime = messageSentTime;
            FileCheckSum = fileCheckSum;
            if (!string.IsNullOrEmpty(fileCheckSum))
                FileName = messageText;
            FilePath = filePath;
        }
        public Message(Message message)
        {
            ChatID = message.ChatID;
            UserID = message.UserID;
            MessageText = message.MessageText;
            MessageReadTime = message.MessageReadTime;
            MessageSentTime = message.MessageSentTime;
            FileCheckSum = message.FileCheckSum;
            FilePath = message.FilePath;
            if (!string.IsNullOrEmpty(FilePath))
                FileName = new FileInfo(FilePath).Name;
        }
        public Message(WCF_Server.DataContracts.MessageContract messageContract)
        {
            MessageID = messageContract.MessageID;
            ChatID = messageContract.ChatID;
            UserID = messageContract.UserID;
            MessageText = messageContract.MessageText;
            MessageReadTime = messageContract.MessageReadTime;
            MessageSentTime = messageContract.MessageSentTime;
            FileCheckSum = messageContract.FileCheckSum;
        }
        #endregion

    }
}
