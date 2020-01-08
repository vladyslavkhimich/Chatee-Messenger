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
        #endregion
        #region Constructor
        public FileListDesignModel() : base()
        {

        }
        #endregion
    }
}
