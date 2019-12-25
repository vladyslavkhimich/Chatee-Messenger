using System;
using System.Collections.Generic;
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
        public bool IsHasFileAttachment => FileCheckSum != string.Empty;
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
        #endregion
        #region Constructors
        public Message()
        {

        }
        public Message(int chatID, int userID, bool isSentByMe, DateTime messageReadTime, DateTime messageSentTime, string messageText = "", string fileID = "", string imageID = "")
        {
            ChatID = chatID;
            UserID = userID;
            MessageText = messageText;
            IsSentByMe = isSentByMe;
            MessageReadTime = messageReadTime;
            MessageSentTime = messageSentTime;
            FileCheckSum = fileID;
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
