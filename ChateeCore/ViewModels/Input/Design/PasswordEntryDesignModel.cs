using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class PasswordEntryDesignModel : PasswordEntryViewModel
    {
        #region Singleton
        public static PasswordEntryDesignModel Instance => new PasswordEntryDesignModel();
        #endregion
        #region Constructors
        public PasswordEntryDesignModel()
        {
            Label = "Name";
            FakePassword = "********";
        }
        #endregion
    }
}
