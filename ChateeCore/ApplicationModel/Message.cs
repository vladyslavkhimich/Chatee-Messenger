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
        public int UserID { get; set; }
        public string MessageText { get; set; }
        public bool IsSentByMe { get; set; }
        public bool IsHasFileAttachment => FileCheckSum != null;
        public bool IsHasImageAttachment 
        {
            get
            {
                if (IsHasFileAttachment)
                    return ExtensionTypesContainer.IsImage(FilePath);
                return false;
            }
        }
        public bool IsMessageRead => MessageReadTime > DateTime.MinValue;
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
        public Message(int chatID, int userID, bool isSentByMe, DateTime messageReadTime, DateTime messageSentTime, string messageText = "", string fileCheckSum = "", string filePath = "")
        {
            ChatID = chatID;
            UserID = userID;
            MessageText = messageText;
            IsSentByMe = isSentByMe;
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
            IsSentByMe = message.IsSentByMe;
            MessageReadTime = message.MessageReadTime;
            MessageSentTime = message.MessageSentTime;
            FileCheckSum = message.FileCheckSum;
            FilePath = message.FilePath;
            if (!string.IsNullOrEmpty(FilePath))
                FileName = new FileInfo(FilePath).Name;
        }
        public Message(WCF_Server.DataContracts.MessageContract messageContract)
        {
            ChatID = messageContract.ChatID;
            UserID = messageContract.UserID;
            MessageText = messageContract.MessageText;
            IsSentByMe = messageContract.IsSentByMe;
            MessageReadTime = messageContract.MessageReadTime;
            MessageSentTime = messageContract.MessageSentTime;
            FileCheckSum = messageContract.FileCheckSum;
        }
        #endregion

    }
}
