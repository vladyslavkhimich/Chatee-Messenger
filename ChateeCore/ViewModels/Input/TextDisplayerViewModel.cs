using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class TextDisplayerViewModel : BaseViewModel
    {
        #region Public Properties
        public string Label { get; set; }
        public string Text { get; set; }
        #endregion
        #region Constructors
        public TextDisplayerViewModel()
        {
            
        }
        #endregion
    }
}
