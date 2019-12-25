using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class TextEntryViewModel : BaseViewModel
    {
        #region Public Properties
        public string Label { get; set; }
        public string OriginalText { get; set; }
        public string EditedText { get; set; }
        public bool IsEditing { get; set; }
        #endregion
        #region Public Commands
        public ICommand EditCommand { get; set; } 
        public ICommand CancelCommand { get; set; } 
        public ICommand SaveCommand { get; set; } 
        #endregion
        #region Constructors
        public TextEntryViewModel()
        {
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
        }
        #endregion
        #region Commands Methods
        public void Edit()
        {
            EditedText = OriginalText;
            IsEditing = true;
        }
        public void Cancel()
        {
            IsEditing = false;
        }
        public void Save()
        {
            OriginalText = EditedText;
            IsEditing = false;
        }
        #endregion
    }
}
