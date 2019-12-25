using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChateeCore
{
    public class SettingsDesignModel : SettingsViewModel
    {
        #region Singleton
        public static SettingsDesignModel Instance => new SettingsDesignModel();
        #endregion
        #region Constructors
        public SettingsDesignModel()
        {
            Name = new TextEntryViewModel { Label = "Name", OriginalText = "Vladyslav Khimich" };
            Username = new TextEntryViewModel { Label = "Username", OriginalText = "Vladyslav" };
            Password = new PasswordEntryViewModel { Label = "Password", FakePassword = "********" };
            Email = new TextEntryViewModel { Label = "Email", OriginalText = "himich01092001@gmail.com" };
        }
        #endregion
    }
}
