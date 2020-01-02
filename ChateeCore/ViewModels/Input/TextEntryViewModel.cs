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
        public bool IsWorking { get; set; }
        public Func<Task<bool>> CommitAction { get; set; }
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
            bool result = true;
            string currentSavedValue = OriginalText;
            RunCommandAsync(() => IsWorking, async () =>
            {
                IsEditing = false;
                OriginalText = EditedText;
                result = CommitAction == null ? true : await CommitAction();
            }).ContinueWith(t =>
            {
                if (!result)
                {
                    OriginalText = currentSavedValue;
                    IsEditing = true;
                }
            });
            OriginalText = EditedText;
            IsEditing = false;
        }
        #endregion
    }
}
