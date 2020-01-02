using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class FileListDesignModel : FileListViewModel
    {
        #region Singleton
        public static FileListDesignModel Instance => new FileListDesignModel();
        public static List<FileAttachment> filesFromDatabase = new List<FileAttachment>
        {
            new FileAttachment("D:/Test/TestFiles/OLX Skis.docx", "D:/Test/TestFiles/OLX Skis.docx", 1),
            new FileAttachment("D:/Test/TestFiles/GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).audio.mp3", "GTA Vice City Flash FM Complete Track (128 kbit s) (via Skyload).audio.mp3", 1),
            new FileAttachment("D:/Test/TestFiles/Panama - The Highs.mp4", "D:/Test/TestFiles/Panama - The Highs.mp4", 1),
            new FileAttachment("D:/Test/TestFiles/Лабораторна робота 17 — копия.pdf", "D:/Test/TestFiles/Лабораторна робота 17 — копия.pdf", 1),
            new FileAttachment("D:/Test/TestFiles/LogoGray.psd", "D:/Test/TestFiles/LogoGray.psd", 1),
            new FileAttachment("D:/Test/TestFiles/Text for birthday.docx", "D:/Test/TestFiles/Text for birthday.docx", 1),
            new FileAttachment("D:/Test/TestFiles/OLX books description.docx", "D:/Test/TestFiles/OLX books description.docx", 1)
        };
        #endregion
        #region Constructor
        public FileListDesignModel() : base(filesFromDatabase)
        {

        }
        #endregion
    }
}
