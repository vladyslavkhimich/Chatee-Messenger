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
        public bool IsMessageSent { get; set; }
        public bool IsMessageRead { get; set; }
        #region Public Commands
        public ICommand DownloadFileCommand { get; set; }
        #endregion
        #region Constructor
        public ChatMessageListItemViewModel()
        {
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        public ChatMessageListItemViewModel(Message message, User user)
        {
            Message = message;
            User = user;
            if (message.FileCheckSum != null && !ExtensionTypesContainer.IsImage(message.FilePath))
                FileAttachment = new ChatMessageListItemFileAttachmentViewModel(message.FilePath, user);
            else if (message.FileCheckSum != null && ExtensionTypesContainer.IsImage(message.FilePath))
                ImageAttachment = new ChatMessageListItemImageAttachmentViewModel(message.FilePath);
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
