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
    public class ChatListItemViewModel : BaseViewModel
    {
        // TODO: Notify if new message available
        #region Public Properties
        public User Interlocutor { get; set; }
        public Chat Chat { get; set; }
        public bool NewMessageAvailable { get; set; }
        public bool IsSelected { get; set; }
        #endregion
        #region Public Commands
        public ICommand OpenMessageCommand { get; set; }
        #endregion
        #region Constructors
        public ChatListItemViewModel()
        {
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }
        public ChatListItemViewModel(User interlocutor, Chat chat)
        {
            Interlocutor = interlocutor;
            Chat = chat;
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }
        #endregion
        #region Commands Methods
        public void OpenMessage()
        {
            IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, new ChatMessageListViewModel(Interlocutor, Chat));
            //User TestUser = new User(1, "Vidzhel", "olezhka228@gmail.com", "Oleg", "Anime is my life", "OT", "FF9466", "Lorem ipsum dor color iotred locusto.");
            //ObservableCollection<Message> TestMessages = new ObservableCollection<Message>();
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, true, DateTime.UtcNow, DateTime.MinValue, "Dummy Message"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Dummy Message", "", "C:/Users/Владелец/source/repos/Chatee/ChateeWPF/Images/Samples/black-tea.png"));
            //TestMessages.Add(new Message(1, 2, false, DateTime.UtcNow, DateTime.MinValue, "Palermo Story.txt", "D:/Test/TestFiles/Palermo Story.txt"));

            //Chat TestChat = new Chat(1, 1, 2, TestMessages);
            //ChatMessageListViewModel TestMessageList = new ChatMessageListViewModel(TestUser, TestChat);
            //ChatMessageListViewModel TestChatMessageViewModel = new ChatMessageListViewModel()
            //{
            //    DisplayTitle = "Oleg",
            //    Messages = new ObservableCollection<ChatMessageListItemViewModel>
            //    {
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = Message,
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "fa0598",
            //            SenderName = "Vladyslav",
            //            IsSentByMe = true,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = "Dummy message",
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "0a0518",
            //            SenderName = "Oleg",
            //            IsSentByMe = false,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = Message,
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "fa0598",
            //            SenderName = "Vladyslav",
            //            IsSentByMe = true,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = "Dummy message",
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "0a0518",
            //            SenderName = "Oleg",
            //            IsSentByMe = false,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = Message,
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "fa0598",
            //            SenderName = "Vladyslav",
            //            IsSentByMe = true,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = "Dummy message",
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "0a0518",
            //            SenderName = "Oleg",
            //            IsSentByMe = false,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = Message,
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "fa0598",
            //            SenderName = "Vladyslav",
            //            IsSentByMe = true,
            //        },
            //        new ChatMessageListItemViewModel
            //        {
            //            Message = "Dummy message",
            //            ImageAttachment = new ChatMessageListItemImageAttachmentViewModel
            //            {
            //                ThumbnailUrl = "http://anywhere"
            //            },
            //            Initials = Initials,
            //            MessageSentTime = DateTimeOffset.UtcNow,
            //            ProfilePictureRGB = "0a0518",
            //            SenderName = "Oleg",
            //            IsSentByMe = false,
            //        },

            //    }
            //};
            //TestChatMessageViewModel.FilteredMessages.Add(
            //    new ChatMessageListItemViewModel
            //    {
            //        Message = "Dummy message",
            //        FileAttachment = new ChatMessageListItemFileAttachmentViewModel
            //        ( "D:/Test/TestFiles/Palermo Story.txt", TestChatMessageViewModel),
            //        Initials = Initials,
            //        MessageSentTime = DateTimeOffset.UtcNow,
            //        ProfilePictureRGB = "0a0518",
            //        SenderName = "Oleg",
            //        IsSentByMe = false,
            //    });
            //IoCContainer.Get<ApplicationViewModel>().GoToPage(ApplicationPages.ChatPage, TestMessageList);
        }
        #endregion
    }
}
