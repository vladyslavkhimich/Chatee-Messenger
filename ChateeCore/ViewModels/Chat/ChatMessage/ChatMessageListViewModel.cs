using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static WCF_Server.DataContracts;

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
        public ApplicationViewModel ApplicationViewModel => IoCContainer.Get<ApplicationViewModel>();
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
        public User Interlocutor { get; set; }
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
        public bool IsMessagesSent { get; set; }
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
        public ICommand SendMessageAsyncCommand { get; set; }
        public ICommand OpenUserInformationCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand ClearSearchCommand { get; set; }
        public ICommand OpenSearchCommand { get; set; }
        public ICommand CloseSearchCommand { get; set; }
        public ICommand SendMessageTestCommand { get; set; }
        #endregion
        #region Constructors
        public ChatMessageListViewModel()
        {
            SetCommandsMethods();
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel(this);
        }
        public ChatMessageListViewModel(User user, Chat chat)
        {
            Interlocutor = user;
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
            SendMessageAsyncCommand = new RelayCommand(SendMessageAsync);
            OpenUserInformationCommand = new RelayCommand(OpenUserInformation);
            SearchCommand = new RelayCommand(Search);
            ClearSearchCommand = new RelayCommand(ClearSearch);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
            SendMessageTestCommand = new RelayCommand(SendMessageTest);
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
        public void SendMessageAsync()
        {
            if (string.IsNullOrEmpty(PendingMessageText) && AttachmentMenu.SelectedFiles.Count == 0)
                return;
            if (Messages == null)
                Messages = new ObservableCollection<ChatMessageListItemViewModel>();
            if (FilteredMessages == null)
            {
                FilteredMessages = new ObservableCollection<ChatMessageListItemViewModel>();
                ApplicationViewModel.ServiceClient.CreateChat(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Interlocutor.UserID);
            }
            Message newMessage;
            if (AttachmentMenu.SelectedFiles.Count > 0)
            {
                for (int i = 0; i < AttachmentMenu.SelectedFiles.Count; i++)
                {
                    newMessage = new Message(Chat.ChatID, ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, true, DateTime.MinValue, DateTime.MinValue, AttachmentMenu.SelectedFiles[i].SelectedFileInfo.Name, FileHelper.ComputeFileCheckSum(AttachmentMenu.SelectedFiles[i].SelectedFileInfo.FullName), AttachmentMenu.SelectedFiles[i].SelectedFileInfo.FullName);
                    RunCommandAsync(() => IsMessagesSent, async () => SendMessage(newMessage));
                }
                AttachmentMenu.SelectedFiles = null;
                AttachmentMenu.IsAttachmentsListVisible = false;
                if (!string.IsNullOrEmpty(PendingMessageText))
                {
                    newMessage = new Message(Chat.ChatID, ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, true, DateTime.MinValue, DateTime.MinValue, PendingMessageText);
                    RunCommandAsync(() => IsMessagesSent, async () => SendMessage(newMessage)).ContinueWith(t => 
                    {
                        PendingMessageText = string.Empty;
                    });
                }
            }
            else
            {
                newMessage = new Message(Chat.ChatID, ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, true, DateTime.MinValue, DateTime.MinValue, PendingMessageText);
                //RunCommandAsync(() => IsMessagesSent, async () => SendMessage(newMessage)).ContinueWith(t => 
                //{
                //    PendingMessageText = string.Empty;
                //});
                Task.Run(() => SendMessage(newMessage));
            }
            //ChatMessageListItemViewModel message;
            //if(AttachmentMenu.SelectedFiles.Count > 0)
            //{
            //    for(int i = 0; i < AttachmentMenu.SelectedFiles.Count; i++)
            //    {
            //        // TODO: Define if file is image and if yes send it without file name
            //        if(ExtensionTypesContainer.IsImage(AttachmentMenu.SelectedFiles[i].SelectedFile.Name))
            //             message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, AttachmentMenu.SelectedFiles[i].SelectedFile.Name, "", AttachmentMenu.SelectedFiles[i].SelectedFile.FullName), User);
            //        else
            //             message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, AttachmentMenu.SelectedFiles[i].SelectedFile.Name, AttachmentMenu.SelectedFiles[i].SelectedFile.FullName, ""), User);
            //        FilteredMessages.Add(message);
            //    }
            //    AttachmentMenu.SelectedFiles = null;
            //    AttachmentMenu.IsAttachmentsListVisible = false;
            //    if (!string.IsNullOrEmpty(PendingMessageText))
            //    {
            //        message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, PendingMessageText), User);
            //        FilteredMessages.Add(message);
            //    }
            //}
            //else
            //{ 
            //    message = new ChatMessageListItemViewModel(new Message(1, 1, true, DateTime.MinValue, DateTime.UtcNow, PendingMessageText), User);                
            //    FilteredMessages.Add(message);
            //}
        }
        public void SendMessage(Message messageToSend)
        {
            ChatMessageListItemViewModel newMessageListItem = new ChatMessageListItemViewModel(messageToSend, Interlocutor);
            RunCommandAsync(() => newMessageListItem.IsMessageSent, async () =>
            {
                ApplicationViewModel.ServiceClient.SendMessage(ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID, Interlocutor.UserID, new MessageContract
                {
                    ChatID = Chat.ChatID,
                    UserID = ApplicationViewModel.CurrentUserContract.ServerDatabaseUserID,
                    MessageText = string.IsNullOrEmpty(newMessageListItem.Message.MessageText) ? string.Empty : newMessageListItem.Message.MessageText,
                    IsSentByMe = true,
                    MessageReadTime = DateTime.MinValue,
                    MessageSentTime = DateTime.UtcNow,
                    FileCheckSum = string.IsNullOrEmpty(newMessageListItem.Message.FileCheckSum) ? string.Empty : newMessageListItem.Message.FileCheckSum,
                    FileName = string.IsNullOrEmpty(newMessageListItem.Message.FileName) ? string.Empty : newMessageListItem.Message.FileName,
                    FileBytes = string.IsNullOrEmpty(newMessageListItem.Message.FilePath) ? null : FileHelper.ConvertFileToArrayOfBytes(newMessageListItem.Message.FilePath)
                });
            });
        }
        public void OpenUserInformation()
        {
            IoCContainer.Get<ApplicationViewModel>().IsUserInformationVisible = true;
            IoCContainer.Get<UserInformationViewModel>().User = Interlocutor;
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
        #region Helper Methods
        public void SendMessageToTheServer(Message message)
        {
            ChatMessageListItemViewModel messageToSend = new ChatMessageListItemViewModel(message, ApplicationViewModel.CurrentUser);
            FilteredMessages.Add(messageToSend);
        }
        public void SendMessageTest()
        {
            ApplicationViewModel.ServiceClient.SendMessage(1, 2, new MessageContract());
        }
        #endregion
    }
}
