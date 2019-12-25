using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class FileListItemViewModel : BaseViewModel
    {
        #region Public Properties
        public FileAttachment File { get; set; }
        public FileInfo FileInfo { get; set; }
        public User User { get; set; }
        public string FileTypeImagePath { get; set; }
        #endregion
        #region Public Commands
        public ICommand DownloadFileCommand { get; set; }
        #endregion
        #region Constructors
        public FileListItemViewModel()
        {
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        // TODO: Get user from database by ID
        public FileListItemViewModel(FileAttachment fileAttachment)
        {
            File = new FileAttachment(fileAttachment.FilePath, fileAttachment.FilePath, fileAttachment.UserID);
            FileInfo = new FileInfo(fileAttachment.FilePath);
            User = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
            FileTypeImagePath = ExtensionTypesContainer.SetFileTypeImage(fileAttachment.FilePath);
        }
        public FileListItemViewModel(FileAttachment fileAttachment, User user)
        {
            File = new FileAttachment(fileAttachment.FilePath, fileAttachment.FilePath, fileAttachment.UserID);
            FileInfo = new FileInfo(fileAttachment.FilePath);
            User = new User(user);
            FileTypeImagePath = ExtensionTypesContainer.SetFileTypeImage(fileAttachment.FilePath);
        }
        #endregion
        #region Commands Methods
        public void DownloadFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.ShowDialog();
        }
        #endregion
    }
}
