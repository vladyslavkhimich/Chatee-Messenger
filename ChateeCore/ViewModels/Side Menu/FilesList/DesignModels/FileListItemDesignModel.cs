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
            
        }
        #endregion
    }
}
