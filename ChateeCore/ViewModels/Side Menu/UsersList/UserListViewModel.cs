using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static WCF_Server.DataContracts;

namespace ChateeCore
{
    public class UserListViewModel : BaseViewModel
    {
        protected string LastSearchText;
        protected string ActualSearchText;
        protected ObservableCollection<UserListItemViewModel> users { get; set; }
        protected ObservableCollection<User> usersFromDatabase { get; set; }
        #region Public Properties
        public ObservableCollection<User> UsersFromDatabase
        {
            get { return usersFromDatabase; }
            set
            {
                if (usersFromDatabase == value)
                    return;
                usersFromDatabase = value;
            } 
        }
        public ObservableCollection<UserListItemViewModel> Users 
        {
            get => users;
            set
            {
                if (users == value)
                    return;
                users = value;
                FilteredUsers = new ObservableCollection<UserListItemViewModel>(users);
            }
        }
        public ObservableCollection<UserListItemViewModel> FilteredUsers { get; set; }
        public string SearchText
        {
            get => ActualSearchText;
            set
            {
                if (ActualSearchText == value)
                    return;
                ActualSearchText = value;
                if (!string.IsNullOrEmpty(SearchText) && SearchText.Length >= 5)
                    Search();
                if (string.IsNullOrEmpty(SearchText) || SearchText.Length < 5)
                    SetUsersFromDatabase(IoCContainer.Get<ApplicationViewModel>().UserInterlocutors.ToList());
            }
        }
        #endregion
        #region Public Commands
        public ICommand ClearSearchCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion
        #region Constructors
        public UserListViewModel()
        {
            ClearSearchCommand = new RelayCommand(ClearSearch);
            SearchCommand = new RelayCommand(Search);
        }
        #endregion
        #region Commands Methods
        public void ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = string.Empty;
                SetUsersFromDatabase(IoCContainer.Get<ApplicationViewModel>().UserInterlocutors.ToList());
            }

        }
        public void Search()
        {
            if ((string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText)) || string.Equals(LastSearchText, SearchText))
                return;
            SetUsersFromDatabase(IoCContainer.Get<ApplicationViewModel>().ServiceClient.GetUsersByUsername(SearchText).ToList());
            LastSearchText = SearchText;
        }
        #endregion
        #region Helper Methods
        public void SetUsersFromDatabase(List<UserContract> userContracts)
        {
            UsersFromDatabase = new ObservableCollection<User>();
            foreach (var userContract in userContracts)
            {
                if (userContract.UserID == IoCContainer.Get<ApplicationViewModel>().CurrentUserContract.UserID)
                    continue;
                UsersFromDatabase.Add(new User(userContract));
            }
            SetUsersCollectionWithDatabaseEntities();
        }
        public void SetUsersCollectionWithDatabaseEntities()
        {
            List<UserListItemViewModel> UsersList = new List<UserListItemViewModel>();
            foreach (var userFromDatabase in UsersFromDatabase)
                UsersList.Add(new UserListItemViewModel(userFromDatabase));
            Users = new ObservableCollection<UserListItemViewModel>(UsersList);
        }
        #endregion
    }
}
