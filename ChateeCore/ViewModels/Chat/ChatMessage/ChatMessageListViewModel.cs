using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChateeCore
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        #region Protected Members
        protected string LastSearchText;
        protected string ActualSearchText;
        protected ObservableCollection<ChatMessageListItemViewModel> messages { get; set; }
        protected bool isSearchOpened { get; set; }
        #endregion
        #region Public Properties
        public Chat Chat { get; set; }
        public ObservableCollection<ChatMessageListItemViewModel> Messages 
        {   get => messages;
            set
            {
                if (messages == value)
                    return;
                messages = value;
                FilteredMessages = new ObservableCollection<ChatMessageListItemViewModel>(messages);
            }
        }
        public ObservableCollection<ChatMessageListItemViewModel> FilteredMessages { get; 
            set; }
        public string DisplayTitle { get; set; }
        public User User { get; set; }
        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }
        public bool IsAttachmentMenuVisible { get; set; } = false;
        
        public bool IsAnyPopupVisible => IsAttachmentMenuVisible;
        public bool IsSearchOpened 
        {   get => isSearchOpened;
            set
            {
                if (isSearchOpened == value)
                    return;
                isSearchOpened = value;
                if (!isSearchOpened)
                    SearchText = string.Empty;
            }
        }
        public string PendingMessageText { get; set; }
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
        public ICommand AttachmentButtonCommand { get; set; }
        public ICommand PopupClickAwayCommand { get; set; }
        public ICommand SendMessageCommand { get; set; }
        public ICommand OpenUserInformationCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }
        public ICommand OpenSearchCommand { get; set; }
        public ICommand CloseSearchCommand { get; set; }
        #endregion
        #region Constructors
        public ChatMessageListViewModel()
        {
            SetCommandsMethods();
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel(this);
        }
        public ChatMessageListViewModel(User user, Chat chat)
        {
            User = user;
            Chat = chat;
            messages = new ObservableCollection<ChatMessageListItemViewModel>();
            foreach (var message in Chat.Messages)
                messages.Add(new ChatMessageListItemViewModel(message, user));
            Messages = new ObservableCollection<ChatMessageListItemViewModel>(messages);
            SetCommandsMethods();
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel(this);
        }
        #endregion
        #region Helper Methods
        public void SetCommandsMethods()
        {
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            PopupClickAwayCommand = new RelayCommand(PopupClickAway);
            SendMessageCommand = new RelayCommand(SendMessage);
            OpenUserInformationCommand = new RelayCommand(OpenUserInformation);
            SearchCommand = new RelayCommand(Search);
            ClearSearchCommand = new RelayCommand(ClearSearch);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
        }
        #endregion
        #region Commands methods
        public void AttachmentButton()
        {
            IsAttachmentMenuVisible ^= true;
        }
        public void PopupClickAway()
        {
            IsAttachmentMenuVisible = false;
        }
        public void SendMessage()
        {
            if (string.IsNullOrEmpty(PendingMessageText) && AttachmentMenu.SelectedFiles.Count == 0)
                return;
            if (Messages == null)
                Messages = new ObservableCollection<ChatMessageListItemViewModel>();
            if (FilteredMessages == null)
                FilteredMessages = new ObservableCollection<ChatMessageListItemViewModel>();
            ChatMessageListItemViewModel message;
            if(AttachmentMenu.SelectedFiles.Count > 0)
            {
                for(int i = 0; i < AttachmentMenu.SelectedFiles.Count; i++)
                {
                    // TODO: Define if file is image and if yes send it without file name
                    if(ExtensionTypesContainer.IsImage(AttachmentMenu.SelectedFiles[i].SelectedFile.Name))
                         message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, AttachmentMenu.SelectedFiles[i].SelectedFile.Name, "", AttachmentMenu.SelectedFiles[i].SelectedFile.FullName), User);
                    else
                         message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, AttachmentMenu.SelectedFiles[i].SelectedFile.Name, AttachmentMenu.SelectedFiles[i].SelectedFile.FullName, ""), User);
                    FilteredMessages.Add(message);
                }
                AttachmentMenu.SelectedFiles = null;
                AttachmentMenu.IsAttachmentsListVisible = false;
                if (!string.IsNullOrEmpty(PendingMessageText))
                {
                    message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, PendingMessageText), User);
                    FilteredMessages.Add(message);
                }
            }
            else
            { 
                message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, PendingMessageText), User);                
                FilteredMessages.Add(message);
            }
            PendingMessageText = string.Empty;
        }
        public void OpenUserInformation()
        {
            IoCContainer.Get<ApplicationViewModel>().IsUserInformationVisible = true;
            IoCContainer.Get<UserInformationViewModel>().User = User;
        }
        public void Search()
        {
            if((string.IsNullOrEmpty(LastSearchText) && string.IsNullOrEmpty(SearchText)) || string.Equals(LastSearchText, SearchText))
                return;
            if(string.IsNullOrEmpty(SearchText) || Messages == null || Messages.Count <= 0)
            {
                FilteredMessages = new ObservableCollection<ChatMessageListItemViewModel>(Messages);
                LastSearchText = SearchText;
                return;
            }
            FilteredMessages = new ObservableCollection<ChatMessageListItemViewModel>(Messages.Where(message => message.Message.MessageText.ToLower().Contains(SearchText)));
            LastSearchText = SearchText;
        }
        public void ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
                SearchText = string.Empty;
            else
                IsSearchOpened = false;
        }
        public void OpenSearch()
        {
            IsSearchOpened = true;
        }
        public void CloseSearch()
        {
            IsSearchOpened = false;
        }

        #endregion
    }
}
