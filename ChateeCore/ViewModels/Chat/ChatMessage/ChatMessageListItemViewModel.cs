using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public bool IsMessageSent { get; set; } = true;
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
            if (!string.IsNullOrEmpty(message.FileCheckSum))
            {
                string localDatabaseFilePath = FileHelper.GetFilePathWithCheckSum(message.FileCheckSum, "/ClientDatabase/Database/FileDatabase/");
                if (string.IsNullOrEmpty(localDatabaseFilePath))
                {
                    FileHelper.SaveFileToLocalDatabase(message.FileName, FileHelper.ConvertFileToArrayOfBytes(message.FilePath));
                    localDatabaseFilePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/ClientDatabase/Database/FileDatabase/" + message.FileName;
                }
                if (!ExtensionTypesContainer.IsImage(localDatabaseFilePath))
                {
                    FileAttachment = new ChatMessageListItemFileAttachmentViewModel(localDatabaseFilePath, user);
                    IoCContainer.Get<FileListViewModel>().Files.Add(new FileListItemViewModel(localDatabaseFilePath, user));
                    IoCContainer.Get<FileListViewModel>().FilteredFiles.Add(new FileListItemViewModel(localDatabaseFilePath, user));
                    Message.IsHasFileAttachment = true;
                }
                else if (ExtensionTypesContainer.IsImage(localDatabaseFilePath))
                {
                    ImageAttachment = new ChatMessageListItemImageAttachmentViewModel(localDatabaseFilePath);
                    IoCContainer.Get<FileListViewModel>().Files.Add(new FileListItemViewModel(localDatabaseFilePath, user));
                    IoCContainer.Get<FileListViewModel>().FilteredFiles.Add(new FileListItemViewModel(localDatabaseFilePath, user));
                    Message.IsHasImageAttachment = true;
                }
            }
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        #endregion
        #region Commands Methods
        public void DownloadFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = $"(*{(FileAttachment != null ? $"{FileAttachment.SelectedFileInfo.Extension}" : $"{ImageAttachment.SelectedImageInfo.Extension}")})|*{(FileAttachment != null ? $"{FileAttachment.SelectedFileInfo.Extension}" : $"{ImageAttachment.SelectedImageInfo.Extension}")}|All files(*.*)|*.*",
                AddExtension = true
            };
            if((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, FileHelper.ConvertFileToArrayOfBytes((FileAttachment != null ? FileAttachment.SelectedFileInfo.FullName : ImageAttachment.SelectedImageInfo.FullName).ToString()));
            }
        }
        #endregion
    }
}
