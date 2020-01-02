using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class FileAttachment
    {
        #region Public Properties
        public string FileName { get; set; }
        public string FileCheckSum { get; set; }
        public string FilePath { get; set; }
        public string LocalDatabasePath { get; set; }
        public int UserID { get; set; }
        public int ChatID { get; set; }
        public bool IsNewFile { get; set; }
        public bool IsDownloaded => File.Exists(LocalDatabasePath);
        #endregion
        #region Constructors
        // TODO: Check in Database if file is new
        public FileAttachment()
        {

        }
        public FileAttachment(string filePath)
        {
            FilePath = filePath;
        }
        public FileAttachment(string fileID, string filePath, int userID)
        {
            FileCheckSum = FileHelper.ComputeFileCheckSum(filePath);
            FilePath = filePath;
            FileName = new FileInfo(filePath).Name;
            UserID = userID;
        }
        public FileAttachment(FileAttachment file)
        {
            FileCheckSum = file.FileCheckSum;
            FilePath = file.FilePath;
            UserID = file.UserID;
        }
        #endregion
    }
}
