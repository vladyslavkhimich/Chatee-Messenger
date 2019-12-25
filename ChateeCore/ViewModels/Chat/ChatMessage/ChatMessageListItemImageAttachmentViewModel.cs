using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatMessageListItemImageAttachmentViewModel : BaseViewModel
    {
        #region Public Properties
        public ChatMessageListViewModel ParentChatMessageList { get; set; }
        public ImageAttachment ImageAttachment { get; set; }
        public string LocalFilePath { get; set; }
        #endregion
        #region Constructors
        public ChatMessageListItemImageAttachmentViewModel()
        {

        }
        public ChatMessageListItemImageAttachmentViewModel(string selectedFilePath)
        {
            ImageAttachment = new ImageAttachment(selectedFilePath);
            LocalFilePath = selectedFilePath;
        }
        public ChatMessageListItemImageAttachmentViewModel(string selectedFilePath, ChatMessageListViewModel parentChatMessageList)
        {
            ParentChatMessageList = parentChatMessageList;
            LocalFilePath = selectedFilePath;
            ImageAttachment = new ImageAttachment(selectedFilePath);
        }
        #endregion

    }
}
