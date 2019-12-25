using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class TextEntryDesignModel : TextEntryViewModel
    {
        #region Singleton
        public static TextEntryDesignModel Instance => new TextEntryDesignModel();
        #endregion
        #region Constructors
        public TextEntryDesignModel()
        {
            Label = "Name";
            OriginalText = "Vladyslav Khimich";
            EditedText = "Editing";
        }
        #endregion
    }
}
