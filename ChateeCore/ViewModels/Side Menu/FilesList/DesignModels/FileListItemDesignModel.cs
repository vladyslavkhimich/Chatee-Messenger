using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class FileListItemDesignModel : FileListItemViewModel
    {
        #region Singleton
        public static FileListItemDesignModel Instance => new FileListItemDesignModel();
        #endregion
        #region Constructor
        public FileListItemDesignModel()
        {
            User = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
            File = new FileAttachment("D:/Test/TestFiles/Palermo Story.txt");
        }
        #endregion
    }
}
