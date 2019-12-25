using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class FileListViewModel : BaseViewModel
    {
        // TODO: Get list of files from database
        // TODO: Notify about changing database files collection Files collection same as in UserListViewModel
        protected string LastSearchText;
        protected string ActualSearchText;
        protected ObservableCollection<FileListItemViewModel> files { get; set; }
        #region Public Properties
        public ObservableCollection<FileAttachment> DatabaseFiles { get; set; }
        public ObservableCollection<FileListItemViewModel> Files 
        {
            get => files;
            set
            {
                if (files == value)
                    return;
                files = value;
                FilteredFiles = new ObservableCollection<FileListItemViewModel>(files);
            }
        }
        public ObservableCollection<FileListItemViewModel> FilteredFiles { get; set; }
        public string SearchText
        {
            get => ActualSearchText;
            set
            {
                if (ActualSearchText == value)
                    return;
                ActualSearchText = value;
                if (string.IsNullOrEmpty(SearchText))
                    Search();
            }
        }
        #endregion
        #region Public Commands
        public ICommand ClearSearchCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion
        #region Constructors
        public FileListViewModel()
        {
            ClearSearchCommand = new RelayCommand(ClearSearch);
            SearchCommand = new RelayCommand(Search);
        }
        public FileListViewModel(List<FileAttachment> files)
        {
            DatabaseFiles = new ObservableCollection<FileAttachment>(files);
            List<FileListItemViewModel> fileListItems = new List<FileListItemViewModel>();
            foreach (var fileFromDatabase in DatabaseFiles)
                fileListItems.Add(new FileListItemViewModel(fileFromDatabase));
            Files = new ObservableCollection<FileListItemViewModel>(fileListItems);
        }
        #endregion
        #region Commands Methods
        public void ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
                SearchText = string.Empty;
        }
        // TODO: Make files search function 
        public void Search()
        {
            if ((string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText)) || string.Equals(LastSearchText, SearchText))
                return;
            LastSearchText = SearchText;
        }
        #endregion
    }
}
