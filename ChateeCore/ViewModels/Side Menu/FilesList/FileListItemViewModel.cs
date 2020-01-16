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
        public FileInfo FileInfo { get; set; }
        public User User { get; set; }
        public string FileCheckSum { get; set; }
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
        public FileListItemViewModel(string filePath, User user)
        {
            FileInfo = new FileInfo(filePath);
            FileCheckSum = FileHelper.ComputeFileCheckSum(filePath);
            User = user;
            FileTypeImagePath = ExtensionTypesContainer.SetFileTypeImage(filePath);
            DownloadFileCommand = new RelayCommand(DownloadFile);
        }
        #endregion
        #region Commands Methods
        public void DownloadFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = $"(*({FileInfo.Extension})|*{FileInfo.Extension}|All files(*.*)|*.*",
                AddExtension = true
            };
            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, FileHelper.ConvertFileToArrayOfBytes(FileInfo.FullName));
            }
        }
        #endregion
    }
}
