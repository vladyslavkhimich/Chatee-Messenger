using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        #region Singleton
        public static ChatMessageListDesignModel Instance => new ChatMessageListDesignModel();
        #endregion
        #region Constructor
        public ChatMessageListDesignModel()
        {
        }
        #endregion
    }
}
