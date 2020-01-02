using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class ChatMessageListItemFileAttachmentViewModel : BaseViewModel
    {
        #region Public Properties
        public ChatMessageListViewModel ParentChatMessageList { get; set; }
        public FileAttachment FileAttachment { get; set; }
        public FileInfo SelectedFileInfo { get; set; }
        public bool IsFileLoading { get; set; }
        public string FileTypeImagePath { get; set; }
        public ICommand DeleteFileCommand { get; set; }
        #endregion
        public ChatMessageListItemFileAttachmentViewModel()
        {

        }
        public ChatMessageListItemFileAttachmentViewModel(string selectedFilePath, User user)
        {
            SelectedFileInfo = new FileInfo(selectedFilePath);
            FileAttachment = new FileAttachment(selectedFilePath, selectedFilePath, user.UserID);
            FileTypeImagePath = ExtensionTypesContainer.SetFileTypeImage(selectedFilePath);
        }
        public ChatMessageListItemFileAttachmentViewModel(string selectedFilePath, ChatMessageListViewModel parentChatMessageList, User user)
        {
            ParentChatMessageList = parentChatMessageList;
            FileAttachment = new FileAttachment(selectedFilePath, selectedFilePath, user.UserID);
            DeleteFileCommand = new RelayCommand(DeleteFile);
            SelectedFileInfo = new FileInfo(selectedFilePath);
            FileTypeImagePath = ExtensionTypesContainer.SetFileTypeImage(selectedFilePath);
        }
        #region Helper Methods
        //public void SetFileTypeImage()
        //{
        //    var extension = SelectedFile.Extension.Replace(".", string.Empty);
        //    if (ExtensionTypesContainer.ExtensionsTypesToStringList.Contains(extension))
        //        FileTypeImagePath += extension + ".png";
        //    else if (string.Equals(extension, "docx"))
        //        FileTypeImagePath += "doc.png";
        //    else
        //        FileTypeImagePath += "default.png";
        //}
        #endregion
        #region Commands Methods
        public void DeleteFile()
        {
            if (ParentChatMessageList.AttachmentMenu.SelectedFiles.Count == 1)
                ParentChatMessageList.AttachmentMenu.IsAttachmentsListVisible = false;
            ParentChatMessageList.AttachmentMenu.SelectedFiles.Remove(this);
        }
        #endregion
    }
}
