using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class ChatMessageListItemViewModel : BaseViewModel
    {
        public Message Message { get; set; }
        public User User { get; set; }
        public ChatMessageListItemImageAttachmentViewModel ImageAttachment { get; set; }
        public ChatMessageListItemFileAttachmentViewModel FileAttachment { get; set; }
        public bool IsHasMessage => Message.MessageText != null;
        #region Public Commands
        public ICommand DownloadFileCommand { get; set; }
        #endregion
        #region Constructor
        public ChatMessageListItemViewModel()
        {
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        public ChatMessageListItemViewModel(Message message, User user, string filePath = "")
        {
            Message = message;
            User = user;
            if (message.FileCheckSum != string.Empty && !ExtensionTypesContainer.IsImage(message.FileCheckSum))
                FileAttachment = new ChatMessageListItemFileAttachmentViewModel(message.FileCheckSum, user);
            else if (message.FileCheckSum != string.Empty && ExtensionTypesContainer.IsImage(message.FileCheckSum))
                ImageAttachment = new ChatMessageListItemImageAttachmentViewModel(message.FileCheckSum);
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        #endregion
        #region Commands Methods
        public void DownloadFile()
        {
            
        }
        #endregion
    }
}
