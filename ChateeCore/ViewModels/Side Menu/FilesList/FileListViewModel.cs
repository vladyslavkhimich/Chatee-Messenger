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
        protected string LastSearchText;
        protected string ActualSearchText;
        protected ObservableCollection<FileListItemViewModel> files { get; set; }
        #region Public Properties
        //public ObservableCollection<FileAttachment> DatabaseFiles { get; set; }
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
                if (!string.IsNullOrEmpty(SearchText))
                    Search();
                else
                    ClearSearch();
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
            Files = new ObservableCollection<FileListItemViewModel>();
        }
        #endregion
        #region Commands Methods
        public void ClearSearch()
        {
            SearchText = string.Empty;
            FilteredFiles = new ObservableCollection<FileListItemViewModel>(Files);
        }
        // TODO: Make files search function 
        public void Search()
        {
            if ((string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText)) || string.Equals(LastSearchText, SearchText))
                return;
            FilteredFiles = new ObservableCollection<FileListItemViewModel>(Files.Where(file => file.FileInfo.Name.ToLower().Contains(SearchText.ToLower())));
            LastSearchText = SearchText;
        }
        #endregion
    }
}
